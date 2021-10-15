using System.Linq;

namespace MagicGradients.Forms.SkiaViews.Drawing
{
    public class GradientPainter
    {
        protected IGradientStop[] GetRenderStops(IGradient gradient)
        {
            var stops = gradient.GetStops();
            if (stops.Count == 1)
            {
                return new[]
                {
                    new GradientStop { RenderOffset = 0, Color = stops[0].Color },
                    new GradientStop { RenderOffset = 1, Color = stops[0].Color }
                };
            }

            return stops.OrderBy(x => x.RenderOffset).ToArray();
        }
    }
}
