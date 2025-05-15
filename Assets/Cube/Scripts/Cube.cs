using Common.Interfaces;
using Cube.Configs;
using LevelService.Scripts;
using UIService.Models;
using UIService.Presenters;
using UnityEngine;

namespace Cube.Scripts
{
  [RequireComponent(typeof(Rigidbody))]
  public sealed class Cube : MonoBehaviour, IObserver
  {
    private CubeConfig _config;
    private CubeEffectsHandler _effectsHandler;
    private CubeInstantiateAnimation _instantiateAnimation;
    private Moves _moves;

    private MeshRenderer _meshRenderer;
    private CubeMovement _movement;
    private Rigidbody _rigidbody;

    private bool _collided;

    public bool OnRightMove { get; private set; }
    public bool OnWrongMove { get; private set; }

    public bool IsSelected { get; private set; }

    public void Construct(CubeConfig config, Level currentLevel, Moves moves = null)
    {
      _meshRenderer = GetComponentInChildren<MeshRenderer>();
      _rigidbody = GetComponent<Rigidbody>();

      _config = config;
      _moves = moves;
      _effectsHandler = new CubeEffectsHandler(_meshRenderer, _config, transform, currentLevel.LevelColor);
      _movement = new CubeMovement(transform, _config, currentLevel, _moves);
      _instantiateAnimation = new CubeInstantiateAnimation(transform, _config);

      _instantiateAnimation.PlayAnimation();
      
      Subscribe();
      Freeze();
    }

    public void Subscribe()
    {
      _movement.OnBounceCompleted += Unselect;
      _movement.OnCollidedWithObstacle += OnCollidedWithObstacle;
      _movement.OnWrongMoveBegin += WrongMoveBegin;
      _movement.OnRightMoveBegin += RightMoveBegin;
      _movement.OnMoveComplete += OnMoveComplete;
      _effectsHandler.OnAnimationComplete += OnAnimationComplete;
    }

    private void OnAnimationComplete()
    {
      Deactivate();
      Unselect();
    }

    public void Unsubscribe()
    {
      _movement.OnBounceCompleted -= Unselect;
      _movement.OnCollidedWithObstacle -= OnCollidedWithObstacle;
      _movement.OnWrongMoveBegin -= WrongMoveBegin;
      _movement.OnRightMoveBegin -= RightMoveBegin;
      _movement.OnMoveComplete -= OnMoveComplete;
      _effectsHandler.OnAnimationComplete -= OnAnimationComplete;
    }


    private void Freeze()
    {
      _rigidbody.isKinematic = true;
      // Freeze positions: x, z
      // Freeze rotations: x, y, z
      _rigidbody.constraints = (RigidbodyConstraints)122;
    }

    private void Unfreeze()
    {
      _rigidbody.isKinematic = false;
      _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void RightMoveBegin()
    {
      OnRightMove = true;
      OnWrongMove = false;
      var tutorial = FindAnyObjectByType<TutorialMessagePresenter>();
      if (tutorial != null)
      {
        tutorial.gameObject.SetActive(false);
      }

      _effectsHandler.PlayRightMoveAnimation();
    }

    private void WrongMoveBegin()
    {
      OnWrongMove = true;
      OnRightMove = false;
      _effectsHandler.PlayWrongMoveAnimation();
    }

    private void OnMoveComplete() => _effectsHandler.PlayPopAnimation();

    public void Interact()
    {
      if (!IsSelected && !_instantiateAnimation.AnimationPlaying())
      {
        Select();
        _movement.Move(Unselect);
      }
    }

    private void OnCollidedWithObstacle(Transform obstacle)
    {
      if (obstacle.TryGetComponent(out Cube cube) && !_collided)
      {
        cube._effectsHandler.PlayCollisionAnimation(transform.forward);
        _collided = true;
      }
    }

    private void Deactivate()
    {
      Unselect();
      Unsubscribe();
      transform.gameObject.SetActive(false);
    }

    private void Unselect()
    {
      Freeze();
      _collided = false;
      IsSelected = false;
    }

    private void Select()
    {
      Unfreeze();
      IsSelected = true;
    }
  }
}