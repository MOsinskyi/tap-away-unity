using CurrentLevelService.Enums;
using CurrentLevelService.Interfaces;
using CurrentLevelService.Scripts;
using LevelService.Factories.Scripts;
using LevelService.Interfaces;

namespace LevelService.Scripts
{
  public sealed class TutorialLevelStrategy : ILevelStrategy
  {
    private readonly ILevelStateStorage _storage;
    private readonly CurrentLevel _currentLevel;
    private readonly ILevelBuilder _builder;
    private readonly TutorialLevelFactory _factory;

    public TutorialLevelStrategy(ILevelStateStorage storage, ILevelBuilder builder, TutorialLevelFactory factory,
      CurrentLevel currentLevel)
    {
      _storage = storage;
      _builder = builder;
      _factory = factory;
      _currentLevel = currentLevel;
    }

    public bool IsApplicable() => _storage.GetState(States.IsTutorial);

    public void BuildLevel()
    {
      _builder
        .WithFactory(_factory)
        .WithLevelIndex(_storage.CurrentLevelIndex)
        .Build(() => _currentLevel.NextLevel());
    }
  }
}