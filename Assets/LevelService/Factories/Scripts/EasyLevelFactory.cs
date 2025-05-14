using System.Collections.Generic;
using System.Linq;
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

        public EasyLevelFactory(IEnumerable<EasyLevel> levels, Transform container)
        {
            _levels = levels.ToList();
            _container = container;
        }

        public Level GetLevel(int index)
        {
            var instance = Instantiate(_levels[index].gameObject, _container);
            var level = instance.GetComponent<EasyLevel>();
            level.Construct();

            return level;
        }
    }
}