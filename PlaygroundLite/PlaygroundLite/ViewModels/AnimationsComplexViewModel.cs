namespace PlaygroundLite.ViewModels
{
    public class AnimationsComplexViewModel : BaseViewModel
    {
        public AnimationItem StoryboardAnimation { get; }
        public AnimationItem PointFrameAnimation { get; }

        public AnimationsComplexViewModel()
        {
            StoryboardAnimation = new AnimationItem("Storyboard Animation");
            PointFrameAnimation = new AnimationItem("Point Frame Animation");
        }
    }
}
