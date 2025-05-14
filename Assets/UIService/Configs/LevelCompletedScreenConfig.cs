using DG.Tweening;
using UIService.Interfaces;
using UnityEngine;

namespace UIService.Configs
{
  [CreateAssetMenu(fileName = "LevelCompletedScreenConfig",
    menuName = "Scriptable Objects/Presenters/LevelCompletedScreenConfig", order = 0)]
  public sealed class LevelCompletedScreenConfig : ScriptableObject, IPresenterConfig
  {
    [field: SerializeField]
    [field: Range(0f, 1f)]
    public float AnimationDuration { get; private set; }

    [field: SerializeField] public Ease Ease { get; private set; }

    [field: SerializeField]
    [field: Range(0f, 5f)]
    public float ShowReplayButtonAfterSeconds { get; private set; }
  }
}