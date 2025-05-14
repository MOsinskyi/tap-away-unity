using System;
using Cube.Configs;
using DG.Tweening;
using UnityEngine;

namespace Cube.Scripts
{
    public sealed class CubeEffectsHandler
    {
        private static readonly int BaseColor = Shader.PropertyToID("_BaseColor");
        private readonly CubeConfig _config;
        private readonly MeshRenderer _meshRenderer;
        private readonly Color _startColor;

        private readonly Vector3 _startPosition;
        private readonly Transform _transform;

        public event Action OnAnimationComplete;

        public CubeEffectsHandler(MeshRenderer meshRenderer, CubeConfig config, Transform transform)
        {
            _meshRenderer = meshRenderer;
            _config = config;
            _transform = transform;
            _startColor = _meshRenderer.material.GetColor(BaseColor);
            _startPosition = _transform.localPosition;
        }

        public void PlayCollisionAnimation(Vector3 punchDirection)
        {
            _transform
                .DOPunchPosition(punchDirection * _config.PunchStrength, _config.PunchDuration, _config.PunchVibrato)
                .SetEase(Ease.Linear)
                .OnComplete(ResetToStartPosition);
        }

        private void ResetToStartPosition()
        {
            _transform.localPosition = _startPosition;
        }

        public void PlayWrongMoveAnimation()
        {
            _meshRenderer?.material
                .DOColor(_config.WrongMoveColor, BaseColor, _config.SwitchColorDuration)
                .OnComplete(ResetColor)
                .SetEase(Ease.Linear);
        }

        public void PlayRightMoveAnimation()
        {
            var animation = DOTween.Sequence();
            animation
                .Append(_meshRenderer?.material
                    .DOColor(_config.RightMoveColor, BaseColor,  _config.SwitchColorDuration)
                    .SetEase(Ease.Linear));
        }

        public void PlayPopAnimation()
        {
            var animation = DOTween.Sequence();
            
            animation
                .Append(_meshRenderer?.transform.DOPunchScale(Vector3.one * _config.PopStrength, _config.PunchDuration))
                .Append(_meshRenderer?.transform.DOScale(Vector3.zero, _config.PopDuration))
                .OnComplete(() => OnAnimationComplete?.Invoke());
        }

        private void ResetColor()
        {
            _meshRenderer?.material
                .DOColor(_startColor, BaseColor, _config.SwitchColorDuration)
                .SetEase(Ease.Linear);
        }
    }
}
