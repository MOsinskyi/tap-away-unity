using GameService.Configs;
using UnityEngine;
using Zenject;

namespace Common.Scripts
{
  public sealed class Bootstrap : MonoBehaviour
  {
    [Inject]
    private void Construct(GameConfig config)
    {
      Application.targetFrameRate = config.TargetFrameRate;
    }
  }
}