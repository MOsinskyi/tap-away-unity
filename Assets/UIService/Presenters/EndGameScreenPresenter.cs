using System;
using System.Collections;
using Common.Interfaces;
using DG.Tweening;
using GameService.Scripts;
using TMPro;
using UIService.Interfaces;
using UnityEngine;

namespace UIService.Presenters
{
    public struct ElementPosition
    {
        public Vector2 End { get; set; }
        public Vector2 Start { get; set; }
    }

    public abstract class EndGameScreenPresenter : MonoBehaviour, IObserver
    {
        [field: SerializeField] public CanvasGroup Panel { get; protected set; }
        [field: SerializeField] public TMP_Text SubHeader { get; protected set; }
        [field: SerializeField] public TMP_Text Header { get; protected set; }
        [field: SerializeField] public RectTransform ReplayButton { get; protected set; }

        private Sequence _elementAnimation;

        protected Sequence Animation;

        protected IPresenterConfig Config;
        protected GameHandler GameHandler;

        private void OnDestroy() => Unsubscribe();

        public abstract void Subscribe();

        public abstract void Unsubscribe();

        protected void Construct(IPresenterConfig config, GameHandler gameHandler)
        {
            Config = config;
            GameHandler = gameHandler;

            Panel.alpha = 0;

            Panel.gameObject.SetActive(false);
            ReplayButton.gameObject.SetActive(false);

            InitializeEndPositions();
            InitializeStartPositions();
            BindListeners();
            Subscribe();
        }

        protected void RestartGame()
        {
            CompleteAnimations();
            GameHandler.RestartGame();
        }

        protected virtual void InitializeEndPositions()
        {
            ReplayButtonPosition.End = ReplayButton.anchoredPosition;
            HeaderPosition.End = Header.rectTransform.anchoredPosition;
            SubHeaderPosition.End = SubHeader.rectTransform.anchoredPosition;
        }

        protected virtual void InitializeStartPositions()
        {
            HeaderPosition.Start = new Vector2(HeaderPosition.End.x, -Screen.height / 2f);
            SubHeaderPosition.Start = new Vector2(SubHeaderPosition.End.x, -Screen.height / 2f);
            ReplayButtonPosition.Start = new Vector2(ReplayButtonPosition.End.x, -Screen.height / 2f);
        }

        protected Sequence ShowElementAnimation(RectTransform element, float duration, ElementPosition position)
        {
            _elementAnimation = DOTween.Sequence();

            _elementAnimation
                .Append(element?.DOAnchorPos(position.End, duration).From(position.Start).SetEase(Config.Ease));

            return _elementAnimation;
        }

        protected Sequence HideElementAnimation(TMP_Text element, float duration, ElementPosition position)
        {
            _elementAnimation = DOTween.Sequence();

            _elementAnimation
                .Append(element?.rectTransform.DOAnchorPos(position.Start, duration)
                    .From(position.End).SetEase(Config.Ease))
                .Join(element?.DOFade(0f, duration).SetEase(Config.Ease));

            return _elementAnimation;
        }

        protected Sequence ShowElementAnimation(TMP_Text element, float duration, ElementPosition position)
        {
            _elementAnimation = DOTween.Sequence();

            element.alpha = 0;

            _elementAnimation
                .Append(element?.rectTransform.DOAnchorPos(position.End, duration)
                    .From(position.Start).SetEase(Config.Ease))
                .Join(element?.DOFade(1f, duration).SetEase(Config.Ease));

            return _elementAnimation;
        }

        protected Sequence HideElementAnimation(RectTransform element, float duration, ElementPosition position)
        {
            _elementAnimation = DOTween.Sequence();

            _elementAnimation
                .Append(element?.DOAnchorPos(position.Start, duration).From(position.End).SetEase(Config.Ease));

            return _elementAnimation;
        }

        protected virtual void PlayShowAnimation(float duration, Action callback = null)
        {
            Animation = DOTween.Sequence();

            Animation
                .Append(Panel.DOFade(1f, duration).SetEase(Config.Ease))
                .Append(ShowElementAnimation(Header, duration, HeaderPosition))
                .Append(ShowElementAnimation(SubHeader, duration, SubHeaderPosition))
                .OnComplete(() => callback?.Invoke());
        }

        protected virtual void PlayHideAnimation(float duration, Action callback = null)
        {
            Animation = DOTween.Sequence();

            Animation
                .Append(HideElementAnimation(ReplayButton, duration, ReplayButtonPosition))
                .Append(HideElementAnimation(SubHeader, duration, SubHeaderPosition))
                .Append(HideElementAnimation(Header, duration, HeaderPosition))
                .OnComplete(() => callback?.Invoke());
        }

        protected virtual void CompleteAnimations()
        {
            Animation?.Complete();
            _elementAnimation?.Complete();
            StopAllCoroutines();
        }

        protected IEnumerator ShowReplayButton(float delay = 0)
        {
            yield return new WaitForSeconds(delay);

            ReplayButton.gameObject.SetActive(true);
            ShowElementAnimation(ReplayButton, Config.AnimationDuration, ReplayButtonPosition);
        }

        protected abstract void BindListeners();

        protected virtual void Show()
        {
            Panel.gameObject.SetActive(true);
            PlayShowAnimation(Config.AnimationDuration, OnShowCompleted);
        }

        protected abstract void OnShowCompleted();

        #region ElementPositions

        protected ElementPosition HeaderPosition;
        protected ElementPosition SubHeaderPosition;
        protected ElementPosition ReplayButtonPosition;

        #endregion
    }
}