namespace MagicGradients.Forms.Animation
{
    public class DimensionsAnimation : PropertyAnimation<Dimensions>
    {
        public override ITweener<Dimensions> Tweener { get; } = new DimensionsTweener();
    }

    public class DimensionsAnimationUsingKeyFrames : PropertyAnimationUsingKeyFrames<Dimensions>
    {
        public override ITweener<Dimensions> Tweener { get; } = new DimensionsTweener();
    }

    public class DimensionsKeyFrame : KeyFrame<Dimensions>
    {

    }
}
