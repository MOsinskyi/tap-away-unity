using UIService.Presenters;
using Zenject;

namespace LevelService.Scripts
{
  public sealed class LevelUIContext
  {
    public MovesCountPresenter MovesCount { get; }
    public CurrentLevelPresenter CurrentLevel { get; }
    public LevelCompletedPresenter LevelCompleted { get; }
    public GameOverPresenter GameOver { get; set; }
    public RankBarPresenter RankBar { get; }
    public RankCompletedPresenter RankCompleted { get; }

    public LevelUIContext(MovesCountPresenter movesCount, CurrentLevelPresenter currentLevel,
      LevelCompletedPresenter levelCompleted, RankBarPresenter rankBar, RankCompletedPresenter rankCompleted,
      GameOverPresenter gameOver = null)
    {
      MovesCount = movesCount;
      CurrentLevel = currentLevel;
      LevelCompleted = levelCompleted;
      GameOver = gameOver;
      RankBar = rankBar;
      RankCompleted = rankCompleted;
    }
  }
}