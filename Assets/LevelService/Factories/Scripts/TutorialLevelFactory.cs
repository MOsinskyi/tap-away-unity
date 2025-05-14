using System.Collections.Generic;
using System.Linq;
using LevelService.Factories.Interfaces;
using LevelService.Scripts;
using UnityEngine;
using static UnityEngine.Object;

namespace LevelService.Factories.Scripts
{
    public class TutorialLevelFactory : IFactory
    {
        private readonly Transform _container;
        private readonly List<TutorialLevel> _levels;
        private readonly RectTransform _tutorialScreen;

        public TutorialLevelFactory(IEnumerable<TutorialLevel> levels, Transform container, RectTransform tutorialScreen)
        {
            _container = container;
            _levels = levels.ToList();
            _tutorialScreen = tutorialScreen;
        }

        public Level GetLevel(int index)
        {
            var instance = Instantiate(_levels[index].gameObject, _container);
            
            var level = instance.GetComponent<TutorialLevel>();
            level.Construct(_tutorialScreen);

            return level;
        }
    }
}