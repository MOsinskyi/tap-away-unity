using System;
using LevelService.Factories.Interfaces;
using LevelService.Scripts;
using UIService.Presenters;

namespace LevelService.Interfaces
{
  public interface ILevelBuilder
  {
    LevelBuilder WithMoves();
    LevelBuilder WithLevelIndex(int levelIndex);
    LevelBuilder WithRankBar();
    LevelBuilder WithGameOver();
    LevelBuilder WithFactory(IFactory factory);
    Level Build(Action onLevelComplete);
  }
}