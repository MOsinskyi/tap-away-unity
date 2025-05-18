using Common.InputService.Scripts;
using Common.Scripts;
using Common.Utils;
using UnityEngine;
using Zenject;

namespace Common.Installers
{
  public class BootstrapInstaller : MonoInstaller
  {
    [SerializeField] private CoroutinePerformer coroutinePerformerPrefab;
    
    public override void InstallBindings()
    {
      BindInputService();
      BindUtils();
      BindBootstrap();
      BindCoroutinePerformer();
    }

    private void BindBootstrap()
    {
      Container
        .Bind<Bootstrap>()
        .AsSingle();
    }

    private void BindUtils()
    {
      Container
        .Bind<UniversalUtils>()
        .AsSingle();
    }

    private void BindInputService()
    {
      if (SystemInfo.deviceType == DeviceType.Handheld)
      {
        Container
          .BindInterfacesAndSelfTo<MobileInput>()
          .AsSingle();
      }
      else
      {
        Container
          .BindInterfacesAndSelfTo<DesktopInput>()
          .AsSingle();
      }
    }
    
    private void BindCoroutinePerformer()
    {
      Container
        .BindInterfacesAndSelfTo<CoroutinePerformer>()
        .FromComponentInNewPrefab(coroutinePerformerPrefab)
        .AsSingle()
        .NonLazy();
    }
  }
}