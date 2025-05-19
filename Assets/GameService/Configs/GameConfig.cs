using System;
using NaughtyAttributes;
using UnityEngine;

namespace GameService.Configs
{
  [CreateAssetMenu(fileName = "GameConfig", menuName = "Scriptable Objects/GameConfig", order = 0)]
  public class GameConfig : ScriptableObject
  {
    [field: SerializeField, Range(0, 240)] public int MaxFrameRate {get; private set;}
    [field: SerializeField, Range(0, 120)] public int TargetFrameRate { get; private set; } = 60;
    [field: SerializeField, Range(0f, 4f)] public float SleepTime { get; private set; }

    public event Action OnClearLevelData;
    public event Action OnGoToMediumLevel;
    
    private bool ButtonCondition => Application.isPlaying;

    [Button, ShowIf(nameof(ButtonCondition))]
    public void ClearProgress() => OnClearLevelData?.Invoke();
    
    [Button, ShowIf(nameof(ButtonCondition))]
    public void GoToMedium() => OnGoToMediumLevel?.Invoke();

    private void OnValidate()
    {
      if (TargetFrameRate > MaxFrameRate)
      {
        MaxFrameRate = TargetFrameRate + 1;
      }
    }
  }
}