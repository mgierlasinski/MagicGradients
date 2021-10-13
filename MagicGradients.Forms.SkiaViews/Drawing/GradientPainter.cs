using System.Linq;

namespace MagicGradients.Forms.SkiaViews.Drawing
{
    public class GradientPainter
    {
        protected GradientStop[] GetRenderStops(Gradient gradient)
        {
            // SkiaSharp needs at least two stops to render single color
            if (gradient.Stops.Count == 1)
            {
                return new[]
                {
                    new GradientStop { RenderOffset = 0, Color = gradient.Stops[0].Color },
                    new GradientStop { RenderOffset = 1, Color = gradient.Stops[0].Color }
                };
            }

            return gradient.Stops.OrderBy(x => x.RenderOffset).ToArray();
        }
    }
}
