using Cube.Configs;
using DG.Tweening;
using UnityEngine;

namespace Cube.Scripts
{
    public sealed class CubeInstantiateAnimation
    {
        private readonly CubeConfig _config;
        private readonly Transform _transform;

        private Vector3 _startPosition;
        private Vector3 _startRotation;
        private Vector3 _startScale;

        private Sequence _animation;
        
        public bool AnimationPlaying() => _animation.IsActive() && _animation.IsPlaying();

        public CubeInstantiateAnimation(Transform transform, CubeConfig config)
        {
            _transform = transform;
            _config = config;
            PrepareAnimation();
        }

        private void PrepareAnimation()
        {
            _startPosition = _transform.position;
            _startRotation = _transform.rotation.eulerAngles;
            _startScale = _transform.localScale;

            _transform.position = new Vector3(_startPosition.x * _config.SpawnRange,
                _startPosition.y * _config.SpawnRange,
                _startPosition.z * _config.SpawnRange);

            _transform.rotation = new Quaternion(0f, 0f, 0f, 0f);

            _transform.localScale = Vector3.zero;
        }

        public void PlayAnimation()
        {
            _animation = DOTween.Sequence();
            
            _animation
                .Append(_transform.DOMove(_startPosition, _config.SpawnDuration))
                .Join(_transform.DOScale(_startScale, _config.SpawnDuration))
                .Join(_transform.DOLocalRotate(_startRotation, _config.SpawnDuration, RotateMode.FastBeyond360));
        }
    }
}