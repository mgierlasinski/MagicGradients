using Xamarin.Forms;

namespace MagicGradients.Forms.Animation
{
    public class ThicknessTweener : ITweener<Thickness>
    {
        public Thickness Tween(Thickness @from, Thickness to, double progress)
        {
            return new Thickness(
                from.Left + (to.Left - from.Left) * progress,
                from.Top + (to.Top - from.Top) * progress,
                from.Right + (to.Right - from.Right) * progress,
                from.Bottom + (to.Bottom - from.Bottom) * progress);
        }
    }

    public class ThicknessAnimation : PropertyAnimation<Thickness>
    {
        public override ITweener<Thickness> Tweener { get; } = new ThicknessTweener();
    }

    public class ThicknessAnimationUsingKeyFrames : PropertyAnimationUsingKeyFrames<Thickness>
    {
        public override ITweener<Thickness> Tweener { get; } = new ThicknessTweener();
    }

    public class ThicknessKeyFrame : KeyFrame<Thickness> { }
}
