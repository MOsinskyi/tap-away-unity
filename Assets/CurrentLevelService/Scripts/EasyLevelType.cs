using CurrentLevelService.Enums;
using CurrentLevelService.Interfaces;
using LevelService.Factories.Scripts;

namespace CurrentLevelService.Scripts
{
  public sealed class EasyLevelType : ILevelType
  {
    private readonly ILevelStateStorage _storage;
    public Types Name => Types.Easy;
    
    public int LevelCount { get; }

    public EasyLevelType(ILevelStateStorage storage, EasyLevelFactory factory)
    {
      _storage = storage;
      LevelCount = factory.LevelCount;
    }

    public bool IsCurrent
    {
      get => _storage.GetState(States.IsEasy);
      set => _storage.SetState(States.IsEasy, value);
    }
  }
}