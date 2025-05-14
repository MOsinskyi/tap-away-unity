using Common.Interfaces;
using DG.Tweening;
using TMPro;
using UIService.Models;
using UnityEngine;

namespace UIService.Presenters
{
    [RequireComponent(typeof(TMP_Text))]
    public sealed class MovesCountPresenter : MonoBehaviour, IObserver
    {
        [Header("ANIMATION")] [Range(0f, 10f)] [SerializeField]
        private float force;

        [Range(0f, 1f)] [SerializeField] private float duration;
        [Range(0, 10)] [SerializeField] private int vibrato;
        [SerializeField] private Ease ease;

        private Tween _animation;

        private Moves _moves;
        private TMP_Text _text;

        private void OnDisable()
        {
            if (_moves != null)
            {
                Unsubscribe();
            }
        }

        private void OnValidate()
        {
            _text = GetComponent<TMP_Text>();
        }

        public void Subscribe()
        {
            _moves.OnMoveSpent += OnMoveCountChanged;
            _moves.OnMovesAdded += OnMoveCountChanged;
        }

        public void Unsubscribe()
        {
            _moves.OnMoveSpent -= OnMoveCountChanged;
            _moves.OnMovesAdded -= OnMoveCountChanged;
        }

        public void Construct(Moves moves = null)
        {
            _moves = moves;
            
            UpdateText();
            
            if (moves != null)
            {
                Subscribe();
            }
            
        }

        private void UpdateText()
        {
            if (_moves != null)
            {
                _text.text = _moves.Count + " Moves";
            }
            else
            {
                _text.gameObject.SetActive(false);
            }
        }

        private void PlayAnimation()
        {
            _animation?.Complete();
            _animation = _text?.transform
                .DOPunchRotation(Vector3.forward * force, duration, vibrato)
                .SetEase(ease);
        }

        private void OnMoveCountChanged()
        {
            UpdateText();
            PlayAnimation();
        }
    }
}