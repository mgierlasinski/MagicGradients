using MagicGradients.Animation.Tween;

namespace MagicGradients.Animation
{
    public class IntegerAnimation : PropertyAnimation<int>
    {
        public override ITweener<int> Tweener { get; } = new IntegerTweener();
    }

    public class IntegerAnimationUsingKeyFrames : PropertyAnimationUsingKeyFrames<int>
    {
        public override ITweener<int> Tweener { get; } = new IntegerTweener();
    }

    public class IntegerKeyFrame : KeyFrame<int>
    {

    }
}
