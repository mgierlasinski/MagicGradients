using Xamarin.Forms;

namespace MagicGradients.Forms.Animation
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
