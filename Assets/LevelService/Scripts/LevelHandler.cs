using System;
using System.Collections.Generic;
using Common.Interfaces;
using Common.Utils;
using CurrentLevelService.Scripts;
using GameService.Configs;
using LevelService.Interfaces;
using UIService.Models;
using UnityEngine.SceneManagement;

namespace LevelService.Scripts
{
  public sealed class LevelHandler : IObserver, IDisposable
  {
    private readonly IEnumerable<ILevelStrategy> _strategies;
    private readonly CurrentLevel _currentLevel;
    private readonly GameConfig _gameConfig;
    private readonly Rank _rank;

    public LevelHandler(IEnumerable<ILevelStrategy> strategies, UniversalUtils utils, CurrentLevel currentLevel, Rank rank)
    {
      _strategies = strategies;
      _gameConfig = utils.ConfigLoader.Load<GameConfig>();
      _currentLevel = currentLevel;
      _rank = rank;

      Subscribe();
      CreateLevel();
    }

    private void CreateLevel()
    {
      foreach (var strategy in _strategies)
      {
        if (strategy.IsApplicable())
        {
          strategy.BuildLevel();
          return;
        }
      }
      
      ClearLevelData();
    }

    private void ClearLevelData()
    {
      _currentLevel.Reset();
      _rank.Reset();
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Subscribe()
    {
      _gameConfig.OnClearLevelData += OnClearLevelData;
      _gameConfig.OnGoToMediumLevel += OnGoToMediumLevel;
    }

    private void OnGoToMediumLevel()
    {
      _currentLevel.GoToMediumLevel();
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnClearLevelData() => ClearLevelData();

    public void Unsubscribe()
    {
      _gameConfig.OnClearLevelData -= OnClearLevelData;
      _gameConfig.OnGoToMediumLevel += OnGoToMediumLevel;
    }

    public void Dispose() => Unsubscribe();
  }
}