using CurrentLevelService.Enums;
using CurrentLevelService.Interfaces;

namespace CurrentLevelService.Scripts
{
  public sealed class TutorialLevelType : ILevelType
  {
    private readonly ILevelStateStorage _storage;
    public Types Name => Types.Tutorial;
    public int LevelCount { get; }

    public TutorialLevelType(ILevelStateStorage storage, int levelCount)
    {
      _storage = storage;
      LevelCount = levelCount;
    }

    public bool IsCurrent
    {
      get => _storage.GetState(States.IsTutorial);
      set => _storage.SetState(States.IsTutorial, value);
    }
  }
}