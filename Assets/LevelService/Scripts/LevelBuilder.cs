using System;
using System.Collections.Generic;
using Common.Utils;
using Cube.Configs;
using Cube.Scripts;
using GameService.Scripts;
using LevelService.Factories.Interfaces;
using LevelService.Interfaces;
using Player.Scripts;
using UIService.Models;

namespace LevelService.Scripts
{
  public sealed class LevelBuilder : ILevelBuilder
  {
    private readonly UniversalUtils _utils;
    private readonly PlayerPointer _playerPointer;
    private readonly LevelUIContext _ui;
    private readonly RankBar _rankBar;
    private readonly GameHandler _gameHandler;

    private IFactory _factory;
    private Moves _moves;

    private List<Cube.Scripts.Cube> _cubes;

    private int _levelIndex;

    private bool _withMoves;
    private bool _withRankBar;
    private bool _withGameOver;

    public LevelBuilder(UniversalUtils utils, PlayerPointer playerPointer, LevelUIContext ui, RankBar rankBar,
      GameHandler gameHandler)
    {
      _utils = utils;
      _playerPointer = playerPointer;
      _ui = ui;
      _rankBar = rankBar;
      _gameHandler = gameHandler;

      _factory = null;
      _moves = null;
    }

    public LevelBuilder WithMoves()
    {
      _withMoves = true;

      return this;
    }

    public LevelBuilder WithLevelIndex(int levelIndex)
    {
      _levelIndex = levelIndex;
      return this;
    }

    public LevelBuilder WithRankBar()
    {
      _withRankBar = true;

      return this;
    }

    public LevelBuilder WithGameOver()
    {
      _withGameOver = true;

      return this;
    }

    public LevelBuilder WithFactory(IFactory factory)
    {
      _factory = factory;

      return this;
    }

    public Level Build(Action onLevelComplete)
    {
      var level = _factory.GetLevel(_levelIndex);

      if (_withMoves)
      {
        _moves = new Moves(level as IFixedMoves);
        _gameHandler.SetMoves(_moves);
        _playerPointer.SetMoves(_moves);
      }

      InitializeCubes(level, _moves);

      var cubeContainer = new CubeContainer(_cubes, _playerPointer, _rankBar, _withRankBar);

      _gameHandler.SetCubeContainer(cubeContainer);

      _ui.MovesCount.Construct(_moves);
      _ui.CurrentLevel.Construct(level);

      _ui.LevelCompleted.Construct(_utils, _gameHandler, onLevelComplete);

      if (_withGameOver)
      {
        _ui.GameOver.SetMoves(_moves);
      }

      if (_withRankBar)
      {
        _ui.RankBar.Show();
      }

      return level;
    }

    private void InitializeCubes(Level level, Moves moves = null)
    {
      _cubes = level.Cubes;
      var cubeConfig = _utils.ConfigLoader.Load<CubeConfig>();
      foreach (var cube in _cubes) cube.Construct(cubeConfig, level, moves);
    }
  }
}