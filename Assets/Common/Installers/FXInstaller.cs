using System.Collections.Generic;
using FXService.LevelCompletedFX.Scripts;
using UnityEngine;
using Zenject;

namespace Common.Installers
{
  public sealed class FXInstaller : MonoInstaller
  {
    [SerializeField] private List<ParticleSystem> levelCompletedFX;
    
    public override void InstallBindings()
    {
      Container.Bind<IEnumerable<ParticleSystem>>().FromInstance(levelCompletedFX).AsSingle();
      Container.Bind<LevelCompletedFX>().AsSingle();
    }
  }
}