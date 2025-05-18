using System;
using System.Collections.Generic;
using System.Linq;
using Common.Interfaces;
using Player.Scripts;
using UIService.Models;
using UnityEngine;

namespace Cube.Scripts
{
  public class CubeContainer : IObserver, IDisposable
  {
    private readonly PlayerPointer _playerPointer;
    private readonly RankBar _rankBar;

    private readonly bool _withRankBar;

    public CubeContainer(IEnumerable<Cube> cubes, PlayerPointer playerPointer, RankBar rankBar, bool withRankBar)
    {
      _playerPointer = playerPointer;
      _rankBar = rankBar;
      _withRankBar = withRankBar;
      CubesCount = cubes.Count();
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
        if (_withRankBar)
        {
          _rankBar.Fill();
        }
      }

      if (CubesCount <= 0)
      {
        OnCubeContainerEmpty?.Invoke();
      }
    }

    public void Dispose()
    {
      Unsubscribe();
    }

    private void OnCubeSelected(Cube obj)
    {
      if (obj.OnRightMove) RemoveCube(obj);
    }
  }
}