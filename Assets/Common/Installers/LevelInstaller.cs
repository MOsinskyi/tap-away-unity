using System.Collections.Generic;
using Common.Utils;
using CurrentLevelService.Interfaces;
using CurrentLevelService.Scripts;
using GameService.Scripts;
using LevelService.Interfaces;
using LevelService.Scripts;
using Zenject;

namespace Common.Installers
{
  public class LevelInstaller : MonoInstaller
  {
    public override void InstallBindings()
    {
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
      Container
        .Bind<IEnumerable<ILevelStrategy>>()
        .FromMethod(_ => new List<ILevelStrategy>
        {
          Container.Instantiate<TutorialLevelStrategy>(),
          Container.Instantiate<EasyLevelStrategy>(),
          Container.Instantiate<MediumLevelStrategy>(),
        })
        .AsSingle();
    }
    

    private void BindLevelBuilder()
    {
      Container
        .Bind<ILevelBuilder>()
        .To<LevelBuilder>()
        .AsSingle();
    }

    private void BindCurrentLevel()
    {
      Container
        .Bind<CurrentLevel>()
        .AsSingle();
    }

    private void BindLevelTypes()
    {
      Container
        .Bind<IEnumerable<ILevelType>>()
        .FromMethod(_ => new List<ILevelType>
        {
          Container.Instantiate<TutorialLevelType>(),
          Container.Instantiate<EasyLevelType>(),
          Container.Instantiate<MediumLevelType>(),
        })
        .AsSingle();
    }

    private void BindLevelStateStorage()
    {
      Container
        .Bind<ILevelStateStorage>()
        .To<PlayerPrefsStorage>()
        .AsSingle();
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
  }
}