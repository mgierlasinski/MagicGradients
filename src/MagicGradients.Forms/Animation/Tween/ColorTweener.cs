using Microsoft.Maui.Graphics;

namespace MagicGradients.Animation.Tween
{
    public class ColorTweener : ITweener<Color>
    {
        public Color Tween(Color @from, Color to, double progress)
        {
            return Color.FromRgb(
                from.Red + (to.Red - from.Red) * progress,
                from.Green + (to.Green - from.Green) * progress,
                from.Blue + (to.Blue - from.Blue) * progress);
        }
    }
}
