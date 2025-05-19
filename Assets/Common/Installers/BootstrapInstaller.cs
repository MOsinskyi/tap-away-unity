using Common.InputService.Scripts;
using Common.Utils;
using UnityEngine;
using Zenject;

namespace Common.Installers
{
  public sealed class BootstrapInstaller : MonoInstaller
  {
    [SerializeField] private CoroutinePerformer coroutinePerformerPrefab;
    
    public override void InstallBindings()
    {
      BindInputService();
      BindUtils();
      BindCoroutinePerformer();
      BindFpsHandler();
    }

    private void BindFpsHandler()
    {
      Container
        .Bind<FpsHandler>()
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
      if (SystemInfo.deviceType == DeviceType.Handheld || Application.isMobilePlatform)
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
        .AsSingle();
    }
  }
}