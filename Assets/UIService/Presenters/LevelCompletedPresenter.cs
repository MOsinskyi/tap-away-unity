using System;
using Common.Interfaces;
using Common.Utils;
using GameService.Scripts;
using UIService.Configs;
using UIService.Interfaces;
using UnityEngine.Diagnostics;
using UnityEngine.UI;
using Zenject;

namespace UIService.Presenters
{
    public sealed class LevelCompletedPresenter : EndGameScreenPresenter, IObserver
    {
        private Action _onLevelCompletedCallback;
        
        public override void Subscribe()
        {
            GameHandler.OnLevelCompleted += OnLevelCompleted;
        }
        
        public void Construct(UniversalUtils utils, GameHandler gameHandler, Action onLevelCompletedCallback)
        {
            var config = utils.ConfigLoader.Load<LevelCompletedScreenConfig>();
            base.Construct(config, gameHandler);
            _onLevelCompletedCallback = onLevelCompletedCallback;
        }

        public override void Unsubscribe()
        {
            if (GameHandler != null)
            {
                GameHandler.OnLevelCompleted -= OnLevelCompleted;
            }
        }

        protected override void BindListeners()
        {
            ReplayButton.GetComponent<Button>().onClick.AddListener(NextLevel);
        }

        protected override void OnShowCompleted()
        {
            StartCoroutine(ShowReplayButton(Config.ShowReplayButtonAfterSeconds));
        }

        private void NextLevel()
        {
            _onLevelCompletedCallback?.Invoke();
            RestartGame();
        }

        private void OnLevelCompleted()
        {
            Show();
        }
    }
}