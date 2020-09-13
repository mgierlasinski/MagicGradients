using Xamarin.Forms;

namespace MagicGradients.Animation.Tween
{
    public class CornerRadiusTweener : ITweener<CornerRadius>
    {
        public CornerRadius Tween(CornerRadius @from, CornerRadius to, double progress)
        {
            return new CornerRadius(
                from.TopLeft + (to.TopLeft - from.TopLeft) * progress,
                from.TopRight + (to.TopRight - from.TopRight) * progress,
                from.BottomLeft + (to.BottomLeft - from.BottomLeft) * progress,
                from.BottomRight + (to.BottomRight - from.BottomRight) * progress);
        }
    }
}
