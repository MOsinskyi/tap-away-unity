using System;
using DG.Tweening;
using NaughtyAttributes;
using TMPro;
using UnityEngine;

namespace UIService.Presenters
{
    public class TutorialMessagePresenter : MonoBehaviour
    {
        [SerializeField] private bool hasTextMessage;
        [SerializeField, Range(0f, 2f), ShowIf(nameof(hasTextMessage))] private float animationDuration;
        [SerializeField, ShowIf(nameof(hasTextMessage))] private TMP_Text text;
        
        [Space(20f)]
        [SerializeField] private bool hasHand;
        [SerializeField, ShowIf(nameof(hasHand))] private HandPresenter handPresenter;

        private Sequence _animation;

        public void Construct()
        {
            if (!hasHand) handPresenter = null;
            if (!hasTextMessage) text = null;

            if (handPresenter)
            {
                handPresenter.Construct();
            }

            if (text)
            {
                text.rectTransform.localScale = Vector3.zero;
                PlayAnimation();
            }
        }

        private void PlayAnimation()
        {
            _animation = DOTween.Sequence();

            _animation
                .Append(text.DOFade(1, animationDuration))
                .Join(text.rectTransform.DOScale(Vector3.one, animationDuration));
        }

        private void OnDisable()
        {
            _animation.Kill(true);
        }
    }
}