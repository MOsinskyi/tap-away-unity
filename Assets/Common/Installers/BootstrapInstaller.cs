using Common.Scripts;
using Common.Utils;
using GameService.Configs;
using Zenject;

namespace Common.Installers
{
  public class BootstrapInstaller : MonoInstaller
  {
    public override void InstallBindings()
    {
      Container.BindInterfacesAndSelfTo<UniversalUtils>().AsSingle();
      Container.Bind<Bootstrap>().AsSingle();
    }
  }
}