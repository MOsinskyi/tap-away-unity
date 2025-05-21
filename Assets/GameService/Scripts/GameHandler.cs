using System;
using System.Collections;
using Common.Interfaces;
using Common.Utils;
using Cube.Scripts;
using FXService.LevelCompletedFX.Scripts;
using GameService.Configs;
using JetBrains.Annotations;
using UIService.Models;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameService.Scripts
{
  public sealed class GameHandler : IObserver, IDisposable
  {
    private readonly CoroutinePerformer _coroutinePerformer;
    private readonly GameConfig _gameConfig;
    private readonly LevelCompletedFX _levelCompletedFX;
    private readonly RankBar _rankBar;

    private Moves _moves;
    private CubeContainer _cubeContainer;

    private bool _isGameEnded;

    public GameHandler(CoroutinePerformer coroutinePerformer, UniversalUtils utils, LevelCompletedFX levelCompletedFX,
      RankBar rankBar)
    {
      _coroutinePerformer = coroutinePerformer;
      _gameConfig = utils.ConfigLoader.Load<GameConfig>();
      _levelCompletedFX = levelCompletedFX;
      _rankBar = rankBar;
      Subscribe();
    }

    public void SetMoves(Moves moves)
    {
      _moves = moves;
      _moves.OnMovesEmpty += OnMovesEmpty;
    }

    public void SetCubeContainer(CubeContainer container)
    {
      _cubeContainer = container;
      _cubeContainer.OnCubeContainerEmpty += OnCubeContainerEmpty;
    }

    public void Subscribe()
    {
      if (_rankBar != null)
      {
        _rankBar.OnBarFilled += OnBarFilled;
        _gameConfig.OnClearLevelData += OnClearLevelData;
      }
    }

    private void OnBarFilled()
    {
      _coroutinePerformer.StartCoroutine(WaitForAnimations());
    }

    public void Unsubscribe()
    {
      if (_moves != null)
      {
        _moves.OnMovesEmpty -= OnMovesEmpty;
      }

      if (_rankBar != null)
      {
        _rankBar.OnBarFilled -= OnBarFilled;
        _gameConfig.OnClearLevelData -= OnClearLevelData;
      }

      if (_cubeContainer != null)
      {
        _cubeContainer.OnCubeContainerEmpty -= OnCubeContainerEmpty;
      }
      
    }

    private void OnClearLevelData() => _rankBar.Reset();

    public event Action OnGameOver;
    public event Action OnLevelCompleted;
    public event Action OnRankCompleted;

    public void RestartGame()
    {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnCubeContainerEmpty()
    {
      _coroutinePerformer.StartCoroutine(WaitForAnimations());
    }

    private void OnMovesEmpty()
    {
      _coroutinePerformer.StartCoroutine(WaitForAnimations());
    }

    public void ResumeGame()
    {
      _isGameEnded = false;
    }

    private IEnumerator WaitForAnimations()
    {
      if (!_isGameEnded)
      {
        _isGameEnded = true;
        
        if (_cubeContainer.CubesCount <= 0)
        {
          _levelCompletedFX.Play();
          yield return new WaitForSeconds(_gameConfig.SleepTime);
          OnLevelCompleted?.Invoke();
        }
        else if (_moves is { Count: 0 } && _cubeContainer.CubesCount > 0)
        {
          yield return new WaitForSeconds(_gameConfig.SleepTime);
          OnGameOver?.Invoke();
        }
        else
        {
          OnRankCompleted?.Invoke();
        }
      }
    }

    public void Dispose()
    {
      Unsubscribe();
    }
  }
}