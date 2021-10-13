using MagicGradients.Animation.Tween;
using Xamarin.Forms;

namespace MagicGradients.Animation
{
    public class ThicknessAnimation : PropertyAnimation<Thickness>
    {
        public override ITweener<Thickness> Tweener { get; } = new ThicknessTweener();
    }

    public class ThicknessAnimationUsingKeyFrames : PropertyAnimationUsingKeyFrames<Thickness>
    {
        public override ITweener<Thickness> Tweener { get; } = new ThicknessTweener();
    }

    public class ThicknessKeyFrame : KeyFrame<Thickness>
    {

    }
}
