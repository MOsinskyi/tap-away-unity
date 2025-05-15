using UIService.Presenters;
using UnityEngine;
using Zenject;

namespace Common.Installers
{
  public class InstallPresenters : MonoInstaller
  {
    [SerializeField] private MovesCountPresenter movesCountPresenter;
    [SerializeField] private LevelCompletedPresenter levelCompletedPresenter;
    [SerializeField] private CurrentLevelPresenter currentLevelPresenter;
    [SerializeField] private GameOverPresenter gameOverPresenter;
    [SerializeField] private RankBarPresenter rankBarPresenter;
    [SerializeField] private RankCompletedPresenter rankCompletedPresenter;

    public override void InstallBindings()
    {
      Container
        .Bind<MovesCountPresenter>()
        .FromInstance(movesCountPresenter)
        .AsSingle();
      Container
        .Bind<LevelCompletedPresenter>()
        .FromInstance(levelCompletedPresenter)
        .AsSingle();
      Container
        .Bind<CurrentLevelPresenter>()
        .FromInstance(currentLevelPresenter)
        .AsSingle();
      Container
        .Bind<GameOverPresenter>()
        .FromInstance(gameOverPresenter)
        .AsSingle();
      Container
        .Bind<RankBarPresenter>()
        .FromInstance(rankBarPresenter)
        .AsSingle();
      Container
        .Bind<RankCompletedPresenter>()
        .FromInstance(rankCompletedPresenter)
        .AsSingle();
    }
  }
}