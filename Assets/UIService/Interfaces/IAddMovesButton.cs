namespace UIService.Interfaces
{
    public interface IAddMovesButton
    {
        public int MovesToAdd { get; }
        public float ButtonAnimationForce { get; }
        public float ButtonAnimationDuration { get; }
        public int ButtonAnimationVibrato { get; }
        public float ButtonAnimationInterval { get; }
        public int ButtonAnimationLoops { get; }
        public bool HideButtonIfAlreadyClicked { get; }
    }
}