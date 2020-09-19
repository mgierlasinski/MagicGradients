namespace PlaygroundLite.ViewModels
{
    public class AnimationsComplexViewModel : AnimationsViewModel
    {
        public AnimationItem StoryboardAnimation { get; }
        public AnimationItem PointFrameAnimation { get; }

        public AnimationsComplexViewModel()
        {
            StoryboardAnimation = CreateAnimation("Storyboard Animation");
            PointFrameAnimation = CreateAnimation("Point Frame Animation");
        }
    }
}
