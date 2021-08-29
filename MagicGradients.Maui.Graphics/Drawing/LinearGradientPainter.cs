using System.Linq;
using Microsoft.Maui.Graphics;

namespace MagicGradients.Maui.Graphics.Drawing
{
    public class LinearGradientPainter : GradientPainter
    {
        public Paint CreatePaint(LinearGradient gradient, DrawContext context)
        {
            var rect = context.RenderRect;

            var renderStops = GetRenderStops(gradient);
            var line = new LinearGradientGeometry(rect, gradient.Angle);
            var start = line.Start;
            var end = line.End;

            if (gradient.IsRepeating)
            {
                var firstOffset = renderStops.FirstOrDefault()?.Offset ?? 0;
                var lastOffset = renderStops.LastOrDefault()?.Offset ?? 1;

                start = line.GetColorPointAt(firstOffset);
                end = line.GetColorPointAt(lastOffset);

                foreach (var stop in renderStops)
                {
                    stop.Offset = line.ScaleWithBias(stop.Offset, firstOffset, lastOffset, 0, 1);
                }
            }

            // Convert pixels to proportional
            var startPoint = new Point(start.X / rect.Width, start.Y / rect.Height);
            var endPoint = new Point(end.X / rect.Width, end.Y / rect.Height);
            
            return new LinearGradientPaintEx(renderStops, startPoint, endPoint, gradient.IsRepeating);
        }
    }
}
