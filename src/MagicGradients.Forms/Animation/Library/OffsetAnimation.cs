namespace MagicGradients.Forms.Animation
{
    public class OffsetTweener : ITweener<Offset>
    {
        public Offset Tween(Offset @from, Offset to, double progress)
        {
            return new Offset(from.Value + (to.Value - from.Value) * progress, from.Type);
        }
    }

    public class OffsetAnimation : PropertyAnimation<Offset>
    {
        public override ITweener<Offset> Tweener { get; } = new OffsetTweener();
    }

    public class OffsetAnimationUsingKeyFrames : PropertyAnimationUsingKeyFrames<Offset>
    {
        public override ITweener<Offset> Tweener { get; } = new OffsetTweener();
    }

    public class OffsetKeyFrame : KeyFrame<Offset> { }
}
