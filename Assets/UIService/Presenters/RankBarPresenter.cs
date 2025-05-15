using System.Collections;
using Common.Interfaces;
using Common.Utils;
using TMPro;
using UIService.Configs;
using UIService.Models;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

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

    [Inject]
    public void Construct(RankBar model, Rank rank, UniversalUtils utils)
    {
      _model = model;
      _rank = rank;
      _config = utils.ConfigLoader.Load<RankConfig>();

      SetCurrentRankText();
      SetNextRankText();
      _fillUnit = 1f / _model.MaxValue;
      progressBar.fillAmount = _model.Value * _fillUnit;

      Subscribe();
    }

    private void UpdateValue(int value)
    {
      StartCoroutine(FillBar(value));
    }

    private IEnumerator FillBar(int targetValue)
    {
      var startFill = progressBar.fillAmount;
      var targetFill = Mathf.Clamp01(targetValue * _fillUnit);
      var delta = Mathf.Abs(targetFill - startFill);
      
      var duration = Mathf.Clamp(delta / _config.BaseFillSpeed, _config.MinFillDuration, _config.MaxFillDuration);

      var time = 0f;
      while (time < duration)
      {
        time += Time.deltaTime;
        var t = time / duration;
        progressBar.fillAmount = Mathf.Lerp(startFill, targetFill, t);
        yield return null;
      }

      progressBar.fillAmount = targetFill;
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
      UpdateValue(value);
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