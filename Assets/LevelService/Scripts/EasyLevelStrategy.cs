using CurrentLevelService.Enums;
using CurrentLevelService.Interfaces;
using CurrentLevelService.Scripts;
using LevelService.Factories.Scripts;
using LevelService.Interfaces;
using UIService.Presenters;

namespace LevelService.Scripts
{
  public sealed class EasyLevelStrategy : ILevelStrategy
  {
    private readonly ILevelStateStorage _storage;
    private readonly CurrentLevel _currentLevel;
    private readonly ILevelBuilder _builder;
    private readonly EasyLevelFactory _factory;

    public EasyLevelStrategy(ILevelStateStorage storage, CurrentLevel currentLevel, ILevelBuilder builder,
      EasyLevelFactory factory)
    {
      _storage = storage;
      _currentLevel = currentLevel;
      _builder = builder;
      _factory = factory;
    }

    public bool IsApplicable() => _storage.GetState(States.IsEasy);

    public void BuildLevel()
    {
      _builder
        .WithFactory(_factory)
        .WithLevelIndex(_storage.CurrentLevelIndex)
        .WithMoves()
        .WithGameOver()
        .Build(() => _currentLevel.NextLevel());
    }
  }
}