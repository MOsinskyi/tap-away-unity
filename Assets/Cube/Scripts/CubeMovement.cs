using System;
using Cube.Configs;
using DG.Tweening;
using LevelService.Scripts;
using UIService.Models;
using UnityEngine;

namespace Cube.Scripts
{
  public sealed class CubeMovement
  {
    private readonly CubeConfig _config;
    private readonly Moves _moves;

    private readonly Transform _transform;
    private readonly float _cubeScale;

    private Vector3 _direction;
    private Transform _obstacle;
    private Vector3 _startPosition;

    public CubeMovement(Transform transform, CubeConfig config, Level currentLevel, Moves moves = null)
    {
      _config = config;
      _moves = moves;
      _transform = transform;
      _direction = Vector3.zero;
      _cubeScale = currentLevel.transform.localScale.x;
    }

    public event Action OnBounceCompleted;
    public event Action<Transform> OnCollidedWithObstacle;
    public event Action OnWrongMoveBegin;
    public event Action OnRightMoveBegin;
    public event Action OnMoveComplete;

    public void Move(Action onObstacleSelectedCallBack)
    {
      _startPosition = _transform.localPosition;
      if (_transform != null)
      {
        if (CanMove(out var hit) || ObstacleOnRightMove(hit.transform))
        {
          MoveForward();
          _moves?.SpentMove();
        }
        else
        {
          if (!ObstacleSelected(hit.transform))
          {
            BounceFromObstacle(hit.transform);
            _moves?.SpentMove();
            return;
          }

          onObstacleSelectedCallBack?.Invoke();
        }
      }
    }

    private void MoveForward()
    {
      OnRightMoveBegin?.Invoke();
      _direction = _transform.forward * _config.MoveDistance + _startPosition;
      _transform
        .DOLocalMove(_direction, _config.Duration)
        .SetEase(_config.MoveEase)
        .OnComplete(() => OnMoveComplete?.Invoke());
    }

    private void BounceFromObstacle(Transform obstacle)
    {
      OnWrongMoveBegin?.Invoke();
      var distance = (_startPosition - obstacle.localPosition).magnitude - _cubeScale;

      if (distance < _config.BounceDistanceThreshold)
      {
        OnBounceCompleted?.Invoke();
        return;
      }

      _direction = Vector3.forward * distance;
      _transform.GetChild(0)
        .DOLocalMove(_direction, _config.Duration * distance / _config.MoveDistance)
        .SetLoops(2, LoopType.Yoyo)
        .OnStepComplete(() => OnCollidedWithObstacle?.Invoke(obstacle))
        .SetEase(_config.MoveEase)
        .OnComplete(() => OnBounceCompleted?.Invoke());
    }

    private bool CanMove(out RaycastHit hit)
    {
      return !Physics.Raycast(_transform.position, _transform.forward, out hit);
    }

    private bool ObstacleOnRightMove(Transform hit)
    {
      return ObstacleSelected(hit) && hit.TryGetComponent(out Cube obstacle) && obstacle.OnRightMove;
    }

    private bool ObstacleSelected(Transform hit)
    {
      return hit.TryGetComponent(out Cube obstacle) && obstacle.IsSelected;
    }
  }
}