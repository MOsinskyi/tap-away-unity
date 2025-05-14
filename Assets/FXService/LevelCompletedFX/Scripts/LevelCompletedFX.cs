using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FXService.LevelCompletedFX.Scripts
{
  public sealed class LevelCompletedFX
  {
    private readonly IEnumerable<ParticleSystem> _effects;
    
    public LevelCompletedFX(IEnumerable<ParticleSystem> effects)
    {
      _effects = effects.ToList();
    }

    public void Play()
    {
      foreach (var effect in _effects)
      {
        effect.Play();
      }
    }
  }
}