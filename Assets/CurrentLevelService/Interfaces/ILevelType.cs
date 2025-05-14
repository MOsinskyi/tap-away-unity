using CurrentLevelService.Enums;

namespace CurrentLevelService.Interfaces
{
  public interface ILevelType
  {
    Types Name { get; }
    int LevelCount { get; }
    bool IsCurrent { get; set; }
  }
}