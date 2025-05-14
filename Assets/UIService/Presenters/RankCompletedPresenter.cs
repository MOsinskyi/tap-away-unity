using Common.Interfaces;
using Common.Utils;
using GameService.Scripts;
using UIService.Configs;
using UIService.Interfaces;
using UIService.Models;
using UnityEngine.UI;

namespace UIService.Presenters
{
  public sealed class RankCompletedPresenter : EndGameScreenPresenter, IObserver
  {
    private Rank _rank;

    public void Construct(Rank rank, UniversalUtils utils, GameHandler gameHandler)
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

    private void Hide()
    {
      PlayHideAnimation(Config.AnimationDuration);
    }

    protected override void OnShowCompleted()
    {
      StartCoroutine(ShowReplayButton(Config.ShowReplayButtonAfterSeconds));
    }
  }
}