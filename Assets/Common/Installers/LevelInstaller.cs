using System.Collections.Generic;
using System.Linq;
using Common.Utils;
using CurrentLevelService.Interfaces;
using CurrentLevelService.Scripts;
using GameService.Scripts;
using LevelService.Factories.Scripts;
using LevelService.Interfaces;
using LevelService.Scripts;
using Player.Scripts;
using Zenject;

namespace Common.Installers
{
  public class LevelInstaller : MonoInstaller
  {
    private ILevelStateStorage _storage;
    private ILevelBuilder _levelBuilder;
    private TutorialLevelFactory _tutorialLevelFactory;
    private EasyLevelFactory _easyLevelFactory;
    private MediumLevelFactory _mediumLevelFactory;
    private CurrentLevel _currentLevel;

    public override void InstallBindings()
    {
      ResolveFactories();
      BindPlayer();
      BindLevelUI();
      BindGameHandler();

      BindLevelStateStorage();
      BindLevelTypes();
      BindCurrentLevel();
      BindLevelBuilder();
      BindLevelStrategies();
      BindLevelHandler();
    }

    private void BindLevelHandler()
    {
      Container
        .Bind<LevelHandler>()
        .AsSingle()
        .NonLazy();
    }

    private void BindLevelStrategies()
    {
      var strategies = new List<ILevelStrategy>()
      {
        new TutorialLevelStrategy(
          _storage,
          _levelBuilder, 
          _tutorialLevelFactory,
          _currentLevel),
        new EasyLevelStrategy(
          _storage,
          _currentLevel,
          _levelBuilder,
          _easyLevelFactory),
        new MediumLevelStrategy(
          _storage,
          _currentLevel,
          _levelBuilder,
          _mediumLevelFactory)
      };

      Container
        .Bind<IEnumerable<ILevelStrategy>>()
        .FromInstance(strategies)
        .AsSingle();
    }

    private void ResolveFactories()
    {
      _tutorialLevelFactory = Container.Resolve<TutorialLevelFactory>();
      _easyLevelFactory = Container.Resolve<EasyLevelFactory>();
      _mediumLevelFactory = Container.Resolve<MediumLevelFactory>();
    }

    private void BindLevelBuilder()
    {
      Container
        .Bind<ILevelBuilder>()
        .To<LevelBuilder>()
        .AsSingle();
      
      _levelBuilder = Container.Resolve<ILevelBuilder>();
    }

    private void BindCurrentLevel()
    {
      Container
        .Bind<CurrentLevel>()
        .AsSingle();
      
      _currentLevel = Container.Resolve<CurrentLevel>();
    }

    private void BindLevelTypes()
    {
      var tutorialLevelsCount = Container.Resolve<IEnumerable<TutorialLevel>>().Count();
      var easyLevelsCount = Container.Resolve<IEnumerable<EasyLevel>>().Count();
      var mediumLevelsCount = Container.Resolve<IEnumerable<MediumLevel>>().Count();
      
      var levelTypes = new List<ILevelType>
      {
        new TutorialLevelType(_storage, tutorialLevelsCount),
        new EasyLevelType(_storage, easyLevelsCount),
        new MediumLevelType(_storage, mediumLevelsCount)
      };

      Container
        .Bind<IEnumerable<ILevelType>>()
        .FromInstance(levelTypes)
        .AsSingle();
    }

    private void BindLevelStateStorage()
    {
      Container
        .Bind<ILevelStateStorage>()
        .To<PlayerPrefsStorage>()
        .AsSingle();
      
      _storage = Container.Resolve<ILevelStateStorage>();
    }

    private void BindGameHandler()
    {
      Container
        .Bind<GameHandler>()
        .AsSingle();
    }

    private void BindLevelUI()
    {
      Container
        .Bind<LevelUIContext>()
        .AsSingle()
        .NonLazy();
    }

    private void BindPlayer()
    {
      Container
        .Bind<PlayerPointer>()
        .FromComponentInHierarchy()
        .AsSingle();
    }
  }
}