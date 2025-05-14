using System.Collections.Generic;
using System.Linq;
using CurrentLevelService.Interfaces;
using UnityEngine;
using Types = CurrentLevelService.Enums.Types;

namespace CurrentLevelService.Scripts
{
  public sealed class CurrentLevel
  {
    private readonly ILevelStateStorage _storage;
    private readonly List<ILevelType> _levelTypes;

    public CurrentLevel(ILevelStateStorage storage, IEnumerable<ILevelType> levels)
    {
      _storage = storage;
      _levelTypes = levels.ToList();
    }

    public void NextLevel()
    {
      var currentType = _levelTypes.FirstOrDefault(t => t.IsCurrent);
      
      if (currentType == null)
      {
        Reset();
        return;
      }

      if (_storage.CurrentLevelIndex < currentType.LevelCount - 1)
      {
        _storage.CurrentLevelIndex++;
      }
      else
      {
        _storage.CurrentLevelIndex = 0;
        currentType.IsCurrent = false;

        var nextIndex = _levelTypes.IndexOf(currentType) + 1;

        if (nextIndex < _levelTypes.Count)
        {
          _levelTypes[nextIndex].IsCurrent = true;
        }
        else
        {
          Reset();
        }
      }
    }

    public void GoToMediumLevel()
    {
      foreach (var type in _levelTypes)
      {
        type.IsCurrent = false;
      }
      
      var mediumLevel = _levelTypes.FirstOrDefault(t => t.Name == Types.Medium);
      
      if (mediumLevel != null)
      {
        _storage.CurrentLevelIndex = 0;
        mediumLevel.IsCurrent = true;
      }
    }

    public void Reset()
    {
      foreach (var type in _levelTypes)
      {
        type.IsCurrent = false;
      }
      
      _storage.CurrentLevelIndex = 0;
      _levelTypes.First().IsCurrent = true;
    }
  }
}