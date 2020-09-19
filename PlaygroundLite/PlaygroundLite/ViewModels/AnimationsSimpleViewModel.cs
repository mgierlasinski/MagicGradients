namespace PlaygroundLite.ViewModels
{
    public class AnimationsSimpleViewModel : AnimationsViewModel
    {
        public AnimationItem DoubleAnimation { get; }
        public AnimationItem ColorAnimation { get; }
        public AnimationItem PointAnimation { get; }
        public AnimationItem DimensionsAnimation { get; }

        public AnimationsSimpleViewModel()
        {
            DoubleAnimation = CreateAnimation("Double Animation");
            ColorAnimation = CreateAnimation("Color Animation");
            PointAnimation = CreateAnimation("Point Animation");
            DimensionsAnimation = CreateAnimation("Dimensions Animation");
        }
    }
}
