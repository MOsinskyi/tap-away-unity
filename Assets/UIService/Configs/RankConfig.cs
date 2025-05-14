using UnityEngine;

namespace UIService.Configs
{
  [CreateAssetMenu(menuName = "Scriptable Objects/Presenters/RankConfig", fileName = "RankConfig",
    order = 0)]
  public sealed class RankConfig : ScriptableObject
  {
    [field: SerializeField, Min(0)] public int MaxRank { get; private set; }
    [field: SerializeField, Min(0)] public int MovesInRank { get; private set; }
  }
}