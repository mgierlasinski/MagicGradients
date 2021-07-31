using System.Linq;
using MauiGradientStop = Microsoft.Maui.Graphics.GradientStop;

namespace MagicGradients.Maui.Graphics.Drawing
{
    public abstract class GradientPainter
    {
        protected MauiGradientStop[] GetRenderStops(Gradient gradient)
        {
            if (gradient.Stops.Count == 1)
            {
                return new[]
                {
                    new MauiGradientStop(0, gradient.Stops[0].Color.ToMauiColor()),
                    new MauiGradientStop(1, gradient.Stops[0].Color.ToMauiColor())
                };
            }

            return gradient.Stops
                .OrderBy(x => x.RenderOffset)
                .Select(x => new MauiGradientStop(x.RenderOffset, x.Color.ToMauiColor()))
                .ToArray();
        }
    }
}
