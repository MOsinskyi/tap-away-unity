using Player.Scripts;
using UnityEngine;
using Zenject;

namespace Common.Installers
{
  public class PlayerInstaller : MonoInstaller
  {
    [SerializeField] private PlayerPointer playerPointer;
    
    public override void InstallBindings()
    {
      BindPlayerPointer();
    }
    
    private void BindPlayerPointer()
    {
      Container
        .Bind<PlayerPointer>()
        .FromInstance(playerPointer)
        .AsSingle();
    }
  }
}