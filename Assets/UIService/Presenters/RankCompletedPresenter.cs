using Common.Interfaces;
using Common.Utils;
using GameService.Scripts;
using UIService.Configs;
using UIService.Models;
using UnityEngine.UI;
using Zenject;

namespace UIService.Presenters
{
  public sealed class RankCompletedPresenter : EndGameScreenPresenter, IObserver
  {
    private Rank _rank;

    [Inject]
    private void Construct(Rank rank, UniversalUtils utils, GameHandler gameHandler)
    {
      _rank = rank;
      var config = utils.ConfigLoader.Load<RankCompletedConfig>();
      base.Construct(config, gameHandler);
    }
        
    public override void Subscribe()
    {
      GameHandler.OnRankCompleted += OnRankCompleted;
    }

    public override void Unsubscribe()
    {
      if (GameHandler != null)
      {
        GameHandler.OnRankCompleted -= OnRankCompleted;
      }
    }

    private void OnRankCompleted()
    {
      SubHeader.text = "You reached rank " + _rank.CurrentRank + "!";
      Show();
    }

    protected override void BindListeners()
    {
      ReplayButton.GetComponent<Button>().onClick.AddListener(Hide);
    }

    protected override void OnShowCompleted()
    {
      StartCoroutine(ShowReplayButton(Config.ShowReplayButtonAfterSeconds));
    }
  }
}