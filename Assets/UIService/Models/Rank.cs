using System;
using Common.Utils;
using UIService.Configs;
using UnityEngine;

namespace UIService.Models
{
  public sealed class Rank
  {
    private readonly RankConfig _config;

    public event Action OnRankIncreased;

    public Rank(UniversalUtils utils)
    {
      _config = utils.ConfigLoader.Load<RankConfig>();
    }

    public int CurrentRank
    {
      get => PlayerPrefs.GetInt("Rank");
      private set
      {
        PlayerPrefs.SetInt("Rank", value); 
        OnRankIncreased?.Invoke();
      }
    }

    public void GoToNextRank()
    {
      if (CurrentRank < _config.MaxRank)
      {
        CurrentRank++;
      }
      else
      {
        CurrentRank = _config.MaxRank;
      }
    }

    public void Reset()
    {
      if (CurrentRank > 0)
      {
        CurrentRank = 0;
      }
    }
  }
}