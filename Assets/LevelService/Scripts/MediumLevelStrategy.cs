using CurrentLevelService.Enums;
using CurrentLevelService.Interfaces;
using CurrentLevelService.Scripts;
using LevelService.Factories.Scripts;
using LevelService.Interfaces;
using UIService.Presenters;

namespace LevelService.Scripts
{
  public sealed class MediumLevelStrategy : ILevelStrategy
  {
    private readonly ILevelStateStorage _storage;
    private readonly CurrentLevel _currentLevel;
    private readonly ILevelBuilder _builder;
    private readonly MediumLevelFactory _factory;

    public MediumLevelStrategy(ILevelStateStorage storage, CurrentLevel currentLevel, ILevelBuilder builder,
      MediumLevelFactory factory)
    {
      _storage = storage;
      _currentLevel = currentLevel;
      _builder = builder;
      _factory = factory;
    }

    public bool IsApplicable() => _storage.GetState(States.IsMedium);

    public void BuildLevel()
    {
      _builder
        .WithFactory(_factory)
        .WithLevelIndex(_storage.CurrentLevelIndex)
        .WithMoves()
        .WithRankBar()
        .WithGameOver()
        .Build(() => _currentLevel.NextLevel());
    }
  }
}