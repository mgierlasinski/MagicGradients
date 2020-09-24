using MagicGradients.Animation.Tween;

namespace MagicGradients.Animation
{
    public class DoubleAnimation : PropertyAnimation<double>
    {
        public override ITweener<double> Tweener { get; } = new DoubleTweener();
    }

    public class DoubleAnimationUsingKeyFrames : PropertyAnimationUsingKeyFrames<double>
    {
        public override ITweener<double> Tweener { get; } = new DoubleTweener();
    }

    public class DoubleKeyFrame : KeyFrame<double>
    {

    }
}
