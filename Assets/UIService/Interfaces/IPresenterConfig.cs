using DG.Tweening;

namespace UIService.Interfaces
{
    public interface IPresenterConfig
    {
        public float AnimationDuration { get; }
        public Ease Ease { get; }
        public float ShowReplayButtonAfterSeconds { get; }
    }
}