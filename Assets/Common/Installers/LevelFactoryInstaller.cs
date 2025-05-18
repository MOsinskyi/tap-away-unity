using LevelService.Factories.Scripts;
using UnityEngine;
using Zenject;

namespace Common.Installers
{
  public class LevelFactoryInstaller : MonoInstaller
  {
    [SerializeField] private Transform levelContainer;
    [SerializeField] private RectTransform tutorialScreen;

    public override void InstallBindings()
    {
      BindLevelContainer();
      BindTutorialScreen();
      
      BindTutorialLevelFactory();
      BindEasyLevelFactory();
      BindMediumLevelFactory();
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

    private void BindTutorialLevelFactory()
    {
      Container
        .Bind<TutorialLevelFactory>()
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