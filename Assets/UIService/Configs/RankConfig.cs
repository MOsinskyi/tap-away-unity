using System;
using UnityEngine;

namespace UIService.Configs
{
  [CreateAssetMenu(menuName = "Scriptable Objects/Presenters/RankConfig", fileName = "RankConfig",
    order = 0)]
  public sealed class RankConfig : ScriptableObject
  {
    [field: SerializeField, Min(0)] public int MaxRank { get; private set; }
    [field: SerializeField, Min(0)] public int MovesInRank { get; private set; }
    
    [field:Header("RANK BAR")]
    [field: SerializeField, Range(0f, 4f)] public float BaseFillSpeed { get; private set; }
    [field: SerializeField, Range(0f, 1f)] public float MinFillDuration { get; private set; }
    [field: SerializeField, Range(0f, 1f)] public float MaxFillDuration { get; private set; }

    private void OnValidate()
    {
      if (MinFillDuration > MaxFillDuration)
      {
        MaxFillDuration = MinFillDuration + .1f;
      }
    }
  }
}