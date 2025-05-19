using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using GameService.Scripts;
using UnityEngine;

namespace LevelService.Scripts
{
  public abstract class Level : MonoBehaviour
  {
    [field: SerializeField] public string LevelName { get; private set; }
    [field: SerializeField] public Color LevelColor { get; private set; }
    public List<Cube.Scripts.Cube> Cubes { get; private set; }

    public void Construct()
    {
      Cubes = GetComponentsInChildren<Cube.Scripts.Cube>().ToList();
      gameObject.name = LevelName;
      DOTween.SetTweensCapacity(Cubes.Count * 4, Cubes.Count * 2);

      CheckRules();
    }

    private void CheckRules()
    {
      if (Application.isEditor)
      {
        var gameRules = new GameRules(Cubes);
        gameRules.CheckCubeIntegrity();
      }
    }
  }
}