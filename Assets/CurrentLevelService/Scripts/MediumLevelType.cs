using CurrentLevelService.Enums;
using CurrentLevelService.Interfaces;
using LevelService.Factories.Scripts;

namespace CurrentLevelService.Scripts
{
  public sealed class MediumLevelType : ILevelType
  {
    private readonly ILevelStateStorage _storage;
    public Types Name => Types.Medium;
    public int LevelCount { get; }

    public MediumLevelType(ILevelStateStorage storage, MediumLevelFactory factory)
    {
      _storage = storage;
      LevelCount = factory.LevelCount;
    }

    public bool IsCurrent
    {
      get => _storage.GetState(States.IsMedium);
      set => _storage.SetState(States.IsMedium, value);
    }
  }
}