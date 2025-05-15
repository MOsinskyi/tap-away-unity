using Common.Utils;
using GameService.Configs;
using UnityEngine;

namespace Common.Scripts
{
  public sealed class Bootstrap
  {
    private Bootstrap(UniversalUtils utils)
    {
      var gameConfig = utils.ConfigLoader.Load<GameConfig>();
      Application.targetFrameRate = gameConfig.TargetFrameRate;
    }
  }
}