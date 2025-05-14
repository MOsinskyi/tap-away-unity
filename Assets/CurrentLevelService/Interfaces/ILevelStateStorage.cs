using CurrentLevelService.Enums;

namespace CurrentLevelService.Interfaces
{
  public interface ILevelStateStorage
  {
    int CurrentLevelIndex { get; set; }
    bool GetState(States key);
    void SetState(States key, bool state);
  }
}