using MagicGradients.Animation.Tween;
using Microsoft.Maui.Graphics;

namespace MagicGradients.Animation
{
    public class ColorAnimation : PropertyAnimation<Color>
    {
        public override ITweener<Color> Tweener { get; } = new ColorTweener();
    }

    public class ColorAnimationUsingKeyFrames : PropertyAnimationUsingKeyFrames<Color>
    {
        public override ITweener<Color> Tweener { get; } = new ColorTweener();
    }

    public class ColorKeyFrame : KeyFrame<Color>
    {

    }
}
