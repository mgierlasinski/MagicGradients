namespace PlaygroundLite.ViewModels
{
    public class AnimationViewModel : BaseViewModel
    {
        public AnimationItem ColorAnimation { get; }
        public AnimationItem PointAnimation { get; }

        public AnimationViewModel()
        {
            ColorAnimation = new AnimationItem("Color Animation");
            PointAnimation = new AnimationItem("Point Animation");
        }
    }
}
