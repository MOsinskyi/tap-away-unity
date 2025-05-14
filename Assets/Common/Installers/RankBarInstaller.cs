using Common.Utils;
using UIService.Configs;
using UIService.Models;
using Zenject;

namespace Common.Installers
{
  public sealed class RankBarInstaller : MonoInstaller
  {
    public override void InstallBindings()
    {
      BindRank();
      BindRankBar();
    }

    private void BindRankBar()
    {
      Container
        .Bind<RankBar>()
        .AsSingle();
    }

    private void BindRank()
    {
      Container
        .Bind<Rank>()
        .AsSingle();
    }
  }
}