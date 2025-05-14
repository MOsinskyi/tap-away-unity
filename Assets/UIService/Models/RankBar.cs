using System;
using Common.Utils;
using UIService.Configs;
using UnityEngine;

namespace UIService.Models
{
  public sealed class RankBar
  {
    private readonly Rank _rank;
    
    public int MaxValue { get; }

    public event Action OnBarFilled;
    public event Action<int> OnValueChanged;

    public RankBar(Rank rank, UniversalUtils utils)
    {
      _rank = rank;
      MaxValue = utils.ConfigLoader.Load<RankConfig>().MovesInRank;
    }
    
    public int Value
    {
      get => PlayerPrefs.GetInt("RankBarValue");
      private set
      {
        PlayerPrefs.SetInt("RankBarValue", value); 
        OnValueChanged?.Invoke(value);
      }
    }


    public void Fill(int value = 1)
    {
      if (value > 0)
      {
        Value += value;
        
        if (Value >= MaxValue)
        {
          _rank.GoToNextRank();
          OnBarFilled?.Invoke();
          Reset();
        }
      }
    }

    public void Reset()
    {
      if (Value > 0)
      {
        Value = 0;
      }
    }
  }
}