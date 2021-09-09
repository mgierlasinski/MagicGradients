using Xamarin.Forms;

namespace MagicGradients.Animation.Tween
{
    public class ColorTweener : ITweener<Color>
    {
        public Color Tween(Color @from, Color to, double progress)
        {
            return Color.FromRgb(
                from.R + (to.R - from.R) * progress,
                from.G + (to.G - from.G) * progress,
                from.B + (to.B - from.B) * progress);
        }
    }
}
