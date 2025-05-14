using DG.Tweening;
using UIService.Interfaces;
using UnityEngine;

namespace UIService.Configs
{
  [CreateAssetMenu(fileName = "GameOverScreenConfig", menuName = "Scriptable Objects/Presenters/GameOverScreenConfig",
    order = 0)]
  public class GameOverScreenConfig : ScriptableObject, IPresenterConfig, IAddMovesButton
  {
    [Header("ADD MOVES BUTTON")] [SerializeField, Range(1, 20)]
    private int movesToAdd;
    
    [SerializeField, Range(0f, 5f)] private float buttonAnimationForce;
    [SerializeField, Range(0f, 1f)] private float buttonAnimationDuration;
    [SerializeField, Range(0, 10)] private int buttonAnimationVibrato;
    [SerializeField, Range(0f, 5f)] private float buttonAnimationInterval;

    [SerializeField, Range(0, 5)] private int buttonAnimationLoop;

    [SerializeField] private bool hideButtonIfAlreadyClicked;

    public int MovesToAdd => movesToAdd;
    public float ButtonAnimationForce => buttonAnimationForce;
    public float ButtonAnimationDuration => buttonAnimationDuration;
    public int ButtonAnimationVibrato => buttonAnimationVibrato;
    public float ButtonAnimationInterval => buttonAnimationInterval;
    public int ButtonAnimationLoops => buttonAnimationLoop;
    public bool HideButtonIfAlreadyClicked => hideButtonIfAlreadyClicked;
    
    [field: SerializeField, Range(0f, 1f)]
    public float AnimationDuration { get; private set; }
    [field: SerializeField] public Ease Ease { get; private set; }
    [field: SerializeField, Range(0f, 5f)]
    public float ShowReplayButtonAfterSeconds { get; private set; }
  }
}