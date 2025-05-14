using CurrentLevelService.Enums;
using CurrentLevelService.Interfaces;
using UnityEngine;

namespace Common.Utils
{
  public sealed class PlayerPrefsStorage : ILevelStateStorage
  {
    public int CurrentLevelIndex
    {
      get => PlayerPrefs.GetInt("CurrentLevel");
      set => PlayerPrefs.SetInt("CurrentLevel", value);
    }

    public bool GetState(States key) => PlayerPrefs.GetInt(key.ToString()) == 1;
    public void SetState(States key, bool state) => PlayerPrefs.SetInt(key.ToString(), state ? 1 : 0);
  }
}