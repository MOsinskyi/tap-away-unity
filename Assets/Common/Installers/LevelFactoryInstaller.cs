using System.Collections.Generic;
using Common.Utils;
using LevelService.Factories.Scripts;
using LevelService.Scripts;
using UnityEngine;
using Zenject;

namespace Common.Installers
{
  public class LevelFactoryInstaller : MonoInstaller
  {
    [SerializeField] private Transform levelContainer;
    [SerializeField] private RectTransform tutorialScreen;
    
    private UniversalUtils _utils;

    public override void InstallBindings()
    {
      _utils = Container.Resolve<UniversalUtils>();
      
      BindLevelContainer();
      BindTutorialScreen();
      
      BindTutorialLevels();
      BindTutorialLevelFactory();
      
      BindEasyLevels();
      BindEasyLevelFactory();
      
      BindMediumLevels();
      BindMediumLevelFactory();
    }

    private void BindEasyLevels()
    {
      var easyLevels = _utils.ResourcesLoader.LoadLevels<EasyLevel>();

      Container
        .Bind<IEnumerable<EasyLevel>>()
        .FromInstance(easyLevels)
        .AsSingle();
    }

    private void BindTutorialScreen()
    {
      Container
        .Bind<RectTransform>()
        .FromInstance(tutorialScreen)
        .AsSingle();
    }

    private void BindMediumLevelFactory()
    {
      Container
        .Bind<MediumLevelFactory>()
        .AsSingle();
    }

    private void BindLevelContainer()
    {
      Container
        .Bind<Transform>()
        .FromInstance(levelContainer)
        .AsSingle();
    }

    private void BindMediumLevels()
    {
      var mediumLevels = _utils.ResourcesLoader.LoadLevels<MediumLevel>();

      Container
        .Bind<IEnumerable<MediumLevel>>()
        .FromInstance(mediumLevels)
        .AsSingle();
    }

    private void BindTutorialLevelFactory()
    {
      Container
        .Bind<TutorialLevelFactory>()
        .AsSingle();
    }

    private void BindTutorialLevels()
    {
      var tutorialLevels = _utils.ResourcesLoader.LoadLevels<TutorialLevel>();

      Container
        .Bind<IEnumerable<TutorialLevel>>()
        .FromInstance(tutorialLevels)
        .AsSingle();
    }

    private void BindEasyLevelFactory()
    {
      Container
        .Bind<EasyLevelFactory>()
        .AsSingle();
    }
  }
}