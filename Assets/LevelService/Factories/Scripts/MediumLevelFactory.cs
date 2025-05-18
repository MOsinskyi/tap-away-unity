using System.Collections.Generic;
using System.Linq;
using Common.Utils;
using LevelService.Factories.Interfaces;
using LevelService.Scripts;
using UnityEngine;

namespace LevelService.Factories.Scripts
{
  public sealed class MediumLevelFactory : IFactory
  {
    private readonly Transform _container;
    private readonly List<MediumLevel> _levels;

    public MediumLevelFactory(UniversalUtils utils, Transform container)
    {
      _container = container;
      _levels = utils.ResourcesLoader.LoadLevels<MediumLevel>();
    }
        
    public Level GetLevel(int index)
    {
      var instance = Object.Instantiate(_levels[index].gameObject, _container);
      var level = instance.GetComponent<MediumLevel>();
      level.Construct();

      return level;
    }

    public int LevelCount => _levels.Count;
  }
}