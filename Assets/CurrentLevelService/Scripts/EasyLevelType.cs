using CurrentLevelService.Enums;
using CurrentLevelService.Interfaces;

namespace CurrentLevelService.Scripts
{
  public sealed class EasyLevelType : ILevelType
  {
    private readonly ILevelStateStorage _storage;
    public Types Name => Types.Easy;
    
    public int LevelCount { get; }

    public EasyLevelType(ILevelStateStorage storage, int levelCount)
    {
      _storage = storage;
      LevelCount = levelCount;
    }

    public bool IsCurrent
    {
      get => _storage.GetState(States.IsEasy);
      set => _storage.SetState(States.IsEasy, value);
    }
  }
}