namespace PlaygroundLite.ViewModels
{
    public class AnimationsSimpleViewModel : BaseViewModel
    {
        public AnimationItem DoubleAnimation { get; }
        public AnimationItem ColorAnimation { get; }
        public AnimationItem PointAnimation { get; }
        public AnimationItem DimensionsAnimation { get; }

        public AnimationsSimpleViewModel()
        {
            DoubleAnimation = new AnimationItem("Double Animation");
            ColorAnimation = new AnimationItem("Color Animation");
            PointAnimation = new AnimationItem("Point Animation");
            DimensionsAnimation = new AnimationItem("Dimensions Animation");
        }
    }
}
