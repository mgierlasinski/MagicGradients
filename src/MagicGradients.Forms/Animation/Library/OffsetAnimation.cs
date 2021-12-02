﻿namespace MagicGradients.Forms.Animation
{
    public class OffsetAnimation : PropertyAnimation<Offset>
    {
        public override ITweener<Offset> Tweener { get; } = new OffsetTweener();
    }

    public class OffsetAnimationUsingKeyFrames : PropertyAnimationUsingKeyFrames<Offset>
    {
        public override ITweener<Offset> Tweener { get; } = new OffsetTweener();
    }

    public class OffsetKeyFrame : KeyFrame<Offset>
    {

    }
}
