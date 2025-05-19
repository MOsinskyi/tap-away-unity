using System.Collections;
using System.Threading;
using GameService.Configs;
using UnityEngine;

namespace Common.Utils
{
  public sealed class FpsHandler
  {
    private readonly GameConfig _config;

    private float _currentFrameTime;

    public FpsHandler(UniversalUtils utils, CoroutinePerformer coroutinePerformer)
    {
      _config = utils.ConfigLoader.Load<GameConfig>();
      _currentFrameTime = Time.realtimeSinceStartup;
            
      QualitySettings.vSyncCount = 0;
      Application.targetFrameRate = _config.MaxFrameRate;

      coroutinePerformer.StartCoroutine(WaitForNextFrame());
    }

    private IEnumerator WaitForNextFrame()
    {
      while (true)
      {
        yield return new WaitForEndOfFrame();
        _currentFrameTime += 1f / _config.TargetFrameRate;
        var time = Time.realtimeSinceStartup;
        var sleepTime = _currentFrameTime - time - .01f;
        if (sleepTime > 0)
        {
          Thread.Sleep((int)sleepTime * 1000);
        }

        while (time < _currentFrameTime)
        {
          time = Time.realtimeSinceStartup;
        }
      }
    }
  }
}