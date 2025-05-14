using System;
using System.Collections.Generic;
using Common.Interfaces;
using Player.Scripts;
using UIService.Models;

namespace Cube.Scripts
{
  public class CubeContainer : IObserver
  {
    private readonly List<Cube> _cubes;
    private readonly PlayerPointer _playerPointer;
    private readonly RankBar _rankBar;

    private readonly bool _withRankBar;

    public CubeContainer(List<Cube> cubes, PlayerPointer playerPointer, RankBar rankBar, bool withRankBar)
    {
      _cubes = cubes;
      _playerPointer = playerPointer;
      _rankBar = rankBar;
      _withRankBar = withRankBar;
      CubesCount = _cubes.Count;
      Subscribe();
    }

    public int CubesCount { get; private set; }

    public void Subscribe()
    {
      _playerPointer.OnCubeSelected += OnCubeSelected;
    }

    public void Unsubscribe()
    {
      _playerPointer.OnCubeSelected -= OnCubeSelected;
    }

    public event Action OnCubeContainerEmpty;

    private void RemoveCube(Cube cube)
    {
      if (CubesCount > 0)
      {
        CubesCount--;
        _cubes.Remove(cube);
        if (_withRankBar)
        {
          _rankBar.Fill();
        }
      }

      if (CubesCount == 0)
      {
        OnCubeContainerEmpty?.Invoke();
        Unsubscribe();
      }
    }

    private void OnCubeSelected(Cube obj)
    {
      if (obj.OnRightMove) RemoveCube(obj);
    }
  }
}