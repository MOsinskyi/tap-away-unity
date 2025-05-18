using System;
using Cube.Configs;
using DG.Tweening;
using UnityEngine;

namespace Cube.Scripts
{
  public sealed class CubeEffectsHandler
  {
    private static readonly int BaseColor = Shader.PropertyToID("_Color");
    private readonly CubeConfig _config;
    private readonly MeshRenderer _meshRenderer;
    private readonly MaterialPropertyBlock _materialPropertyBlock;

    private readonly Color _startColor;
    private readonly Vector3 _startPosition;
    private readonly Transform _transform;

    public event Action OnAnimationComplete;

    public CubeEffectsHandler(MeshRenderer meshRenderer, CubeConfig config, Transform transform, Color startColor)
    {
      _meshRenderer = meshRenderer;
      _config = config;
      _transform = transform;
      _startPosition = _transform.localPosition;
      _materialPropertyBlock = new MaterialPropertyBlock();

      SetColor(startColor);

      if (_meshRenderer != null)
      {
        GetColor(out var color);
        _startColor = color;
      }
        
    }

    public void PlayCollisionAnimation(Vector3 punchDirection)
    {
      _transform
        .DOPunchPosition(punchDirection * _config.PunchStrength, _config.PunchDuration, _config.PunchVibrato)
        .SetEase(Ease.Linear)
        .OnComplete(ResetToStartPosition);
    }

    private void ResetToStartPosition()
    {
      _transform.localPosition = _startPosition;
    }

    public void PlayWrongMoveAnimation()
    {
      GetColor(out var color);

      DOTween.To(
        () => color,
        SetColor,
        _config.WrongMoveColor,
        _config.SwitchColorDuration
      ).SetEase(Ease.Linear).OnComplete(ResetColor);
    }
    
    public void PlayRightMoveAnimation()
    {
      GetColor(out var color);
      
      DOTween.To(
        () => color,
        SetColor,
        _config.RightMoveColor,
        _config.SwitchColorDuration
      ).SetEase(Ease.Linear);
      
    }

    public void PlayPopAnimation()
    {
      var animation = DOTween.Sequence();

      var transform = _meshRenderer?.transform;
      
      animation
        .Append(transform.DOPunchScale(Vector3.one * _config.PopStrength, _config.PunchDuration))
        .Append(transform.DOScale(Vector3.zero, _config.PopDuration))
        .OnComplete(() => OnAnimationComplete?.Invoke());
    }

    private void ResetColor()
    {
      GetColor(out var color);

      DOTween.To(
        () => color,
        SetColor,
        _startColor,
        _config.SwitchColorDuration
      ).SetEase(Ease.Linear);
      
    }

    private void GetColor(out Color color)
    {
      _meshRenderer.GetPropertyBlock(_materialPropertyBlock);
      color = _materialPropertyBlock.GetColor(BaseColor);
    }

    private void SetColor(Color color)
    {
      _materialPropertyBlock.SetColor(BaseColor, color);
      _meshRenderer.SetPropertyBlock(_materialPropertyBlock);
    }
  }
}