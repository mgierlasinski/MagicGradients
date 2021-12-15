namespace MagicGradients.Forms.Animation
{
    public class IntegerTweener : ITweener<int>
    {
        public int Tween(int @from, int to, double progress)
        {
            return (int)(from + (to - from) * progress);
        }
    }

    public class IntegerAnimation : PropertyAnimation<int>
    {
        public override ITweener<int> Tweener { get; } = new IntegerTweener();
    }

    public class IntegerAnimationUsingKeyFrames : PropertyAnimationUsingKeyFrames<int>
    {
        public override ITweener<int> Tweener { get; } = new IntegerTweener();
    }

    public class IntegerKeyFrame : KeyFrame<int> { }
}
