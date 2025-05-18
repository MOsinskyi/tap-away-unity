using System.Collections.Generic;
using System.Linq;
using Common.Utils;
using LevelService.Factories.Interfaces;
using LevelService.Scripts;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Object;

namespace LevelService.Factories.Scripts
{
    public sealed class EasyLevelFactory : IFactory
    {
        private readonly Transform _container;
        private readonly List<EasyLevel> _levels;

        public EasyLevelFactory(UniversalUtils utils, Transform container)
        {
            _levels = utils.ResourcesLoader.LoadLevels<EasyLevel>();
            _container = container;
        }

        public Level GetLevel(int index)
        {
            var instance = Instantiate(_levels[index].gameObject, _container);
            var level = instance.GetComponent<EasyLevel>();
            level.Construct();

            return level;
        }

        public int LevelCount => _levels.Count;
    }
}