using System.Collections.Generic;
using System.Linq;
using LevelService.Factories.Interfaces;
using LevelService.Scripts;
using UnityEngine;

namespace LevelService.Factories.Scripts
{
  public sealed class MediumLevelFactory : IFactory
  {
    private readonly Transform _container;
    private readonly List<MediumLevel> _levels;

    public MediumLevelFactory(IEnumerable<MediumLevel> levels, Transform container)
    {
      _container = container;
      _levels = levels.ToList();
    }
        
    public Level GetLevel(int index)
    {
      var instance = Object.Instantiate(_levels[index].gameObject, _container);
      var level = instance.GetComponent<MediumLevel>();
      level.Construct();

      return level;
    }
  }
}