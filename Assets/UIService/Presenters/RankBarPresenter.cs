using Common.Interfaces;
using TMPro;
using UIService.Configs;
using UIService.Models;
using UnityEngine;
using UnityEngine.UI;

namespace UIService.Presenters
{
  public class RankBarPresenter : MonoBehaviour, IObserver
  {
    [SerializeField] private TMP_Text currentRankText;
    [SerializeField] private TMP_Text nextRankText;
    [SerializeField] private Image progressBar;

    private RankBar _model;
    private Rank _rank;
    private RankConfig _config;

    private float _fillUnit;

    public void Construct(RankBar model, Rank rank, RankConfig config)
    {
      _model = model;
      _rank = rank;
      _config = config;

      SetCurrentRankText();
      SetNextRankText();
      _fillUnit = 1f / _model.MaxValue;
      progressBar.fillAmount = _model.Value * _fillUnit;

      Subscribe();
    }

    private void UpdateValueText(int value)
    {
      progressBar.fillAmount = value * _fillUnit;
    }

    private void SetNextRankText()
    {
      nextRankText.text = _rank.CurrentRank < _config.MaxRank
        ? (_rank.CurrentRank + 1).ToString()
        : _config.MaxRank.ToString();
    }

    private void SetCurrentRankText()
    {
      currentRankText.text = _rank.CurrentRank.ToString();
    }

    public void Show() => gameObject.SetActive(true);
    public void Hide() => gameObject.SetActive(false);

    public void Subscribe()
    {
      _model.OnValueChanged += OnValueChanged;
      _rank.OnRankIncreased += OnRankIncreased;
    }

    private void OnRankIncreased()
    {
      SetCurrentRankText();
      SetNextRankText();
    }

    private void OnValueChanged(int value)
    {
      UpdateValueText(value);
    }

    public void Unsubscribe()
    {
      _model.OnValueChanged -= OnValueChanged;
      _rank.OnRankIncreased -= OnRankIncreased;
    }

    private void OnDestroy()
    {
      Unsubscribe();
    }
  }
}