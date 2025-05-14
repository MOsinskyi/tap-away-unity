using DG.Tweening;
using UIService.Interfaces;
using UnityEngine;

namespace UIService.Configs
{
  [CreateAssetMenu(fileName = "RankCompletedConfig",
    menuName = "Scriptable Objects/Presenters/RankCompletedConfig", order = 0)]
  public sealed class RankCompletedConfig : ScriptableObject, IPresenterConfig
  {
    [field:SerializeField, Range(0f, 1f)] public float AnimationDuration { get; private set; }
    [field:SerializeField] public Ease Ease { get; private set; }
    [field:SerializeField, Range(0f, 5f)] public float ShowReplayButtonAfterSeconds { get; private set; }
  }
}