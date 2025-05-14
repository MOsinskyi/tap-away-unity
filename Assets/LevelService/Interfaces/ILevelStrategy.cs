namespace LevelService.Interfaces
{
  public interface ILevelStrategy
  {
    bool IsApplicable();
    void BuildLevel();
  }
}