using System;
using Common.Utils;
using DG.Tweening;
using GameService.Scripts;
using TMPro;
using UIService.Configs;
using UIService.Interfaces;
using UIService.Models;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UIService.Presenters
{
  public sealed class GameOverPresenter : EndGameScreenPresenter
  {
    [SerializeField] private RectTransform getMovesButton;

    private Sequence _addMovesButtonAnimation;
    private IAddMovesButton _addMovesButtonConfig;
    private Moves _moves;
    private GameOverScreenConfig _config;
    private GameHandler _gameHandler;

    private ElementPosition _getMovesButtonPosition;

    private bool _addMovesClicked;

    private void OnDisable()
    {
      if (_moves != null)
      {
        Unsubscribe();
      }
    }

    [Inject]
    private void Construct(UniversalUtils utils, GameHandler gameHandler)
    {
      _config = utils.ConfigLoader.Load<GameOverScreenConfig>();
      _gameHandler = gameHandler;
      base.Construct(_config, _gameHandler);
    }

    public GameOverPresenter SetMoves(Moves moves)
    {
      _moves = moves;
      _addMovesButtonConfig = Config as IAddMovesButton;
      UpdateMovesTextOnButton();
      Subscribe();

      return this;
    }

    private void UpdateMovesTextOnButton()
    {
      getMovesButton.GetComponentInChildren<TMP_Text>().text = $"+{_addMovesButtonConfig.MovesToAdd} Moves";
    }

    protected override void BindListeners()
    {
      ReplayButton.GetComponent<Button>().onClick.AddListener(RestartGame);
      getMovesButton.GetComponent<Button>().onClick.AddListener(AddMoves);
    }

    private void AddMoves()
    {
      _moves.AddMoves(_addMovesButtonConfig.MovesToAdd);
      _addMovesClicked = true;
      _gameHandler.ResumeGame();
      Hide();
    }

    protected override void InitializeEndPositions()
    {
      base.InitializeEndPositions();
      _getMovesButtonPosition.End = getMovesButton.anchoredPosition;
    }

    protected override void InitializeStartPositions()
    {
      base.InitializeStartPositions();
      _getMovesButtonPosition.Start = new Vector2(_getMovesButtonPosition.End.x, -Screen.height);
    }

    protected override void PlayShowAnimation(float duration, Action callback = null)
    {
      base.PlayShowAnimation(duration, callback);

      Animation
        .OnStart(HideGetMovesButton)
        .Append(ShowElementAnimation(getMovesButton, duration, _getMovesButtonPosition));
    }

    private void HideGetMovesButton()
    {
      if (ButtonHidden()) getMovesButton.gameObject.SetActive(false);
    }

    private void PlayButtonAnimation()
    {
      _addMovesButtonAnimation = DOTween.Sequence();

      _addMovesButtonAnimation
        .SetLoops(_addMovesButtonConfig.ButtonAnimationLoops)
        .Append(getMovesButton?.DOPunchRotation(Vector3.forward * _addMovesButtonConfig.ButtonAnimationForce,
          _addMovesButtonConfig.ButtonAnimationDuration, _addMovesButtonConfig.ButtonAnimationVibrato))
        .AppendInterval(_addMovesButtonConfig.ButtonAnimationInterval);
    }

    protected override void OnShowCompleted()
    {
      if (ButtonHidden())
      {
        StartCoroutine(ShowReplayButton());
      }
      else
      {
        PlayButtonAnimation();
        StartCoroutine(ShowReplayButton(Config.ShowReplayButtonAfterSeconds));
      }
    }

    private bool ButtonHidden()
    {
      return _addMovesClicked && _addMovesButtonConfig.HideButtonIfAlreadyClicked;
    }

    protected override void PlayHideAnimation(float duration, Action callback = null)
    {
      base.PlayHideAnimation(duration, callback);

      Animation
        .Insert(Config.AnimationDuration,
          HideElementAnimation(getMovesButton, duration, _getMovesButtonPosition));
    }

    protected override void CompleteAnimations()
    {
      base.CompleteAnimations();
      _addMovesButtonAnimation?.Kill(true);
    }

    public override void Subscribe()
    {
      GameHandler.OnGameOver += OnGameOver;
    }

    private void OnGameOver()
    {
      Show();
    }

    public override void Unsubscribe()
    {
      if (GameHandler != null)
      {
        GameHandler.OnGameOver -= OnGameOver;
      }
    }
  }
}