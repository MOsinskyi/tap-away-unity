using Common.InputService.Scripts;
using Common.Scripts;
using Common.Utils;
using UnityEngine;
using Zenject;
using SystemInfo = UnityEngine.Device.SystemInfo;

namespace Common.Installers
{
  public class BootstrapInstaller : MonoInstaller
  {
    [SerializeField] private CoroutinePerformer coroutinePerformerPrefab;
    
    public override void InstallBindings()
    {
      BindInputService();
      BindCoroutinePerformer();
      BindUtils();
      BindBootstrap();
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
      var coroutinePerformer = Container
        .InstantiatePrefabForComponent<CoroutinePerformer>(coroutinePerformerPrefab.gameObject);

      Container
        .BindInterfacesAndSelfTo<CoroutinePerformer>()
        .FromInstance(coroutinePerformer)
        .AsSingle();
    }
  }
}