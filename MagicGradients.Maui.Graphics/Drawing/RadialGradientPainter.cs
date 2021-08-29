using System;
using System.Linq;
using Microsoft.Maui.Graphics;

namespace MagicGradients.Maui.Graphics.Drawing
{
    public class RadialGradientPainter : GradientPainter
    {
        public Paint CreatePaint(RadialGradient gradient, DrawContext context)
        {
            var rect = context.RenderRect;

            var renderStops = GetRenderStops(gradient);
            var lastOffset = gradient.IsRepeating ? renderStops.LastOrDefault()?.Offset ?? 1 : 1;

            foreach (var stop in renderStops)
            {
                stop.Offset = lastOffset > 0 ? stop.Offset / lastOffset : 0;
            }

            var circle = new RadialGradientGeometry(gradient, rect, lastOffset, context.PixelScaling);

            // Convert pixels to proportional
            var center = new Point(circle.Center.X / rect.Width, circle.Center.Y / rect.Height);
            var radius = new Size(circle.Radius.Width / rect.Width, circle.Radius.Height / rect.Height);
            
            return new RadialGradientPaintEx(renderStops, center, radius, gradient.IsRepeating);
        }
    }
}
