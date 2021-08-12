using System;
using Microsoft.Maui.Graphics;

namespace MagicGradients.Maui.Graphics.Drawing
{
    public class RadialGradientPainter : GradientPainter
    {
        public Paint CreatePaint(RadialGradient gradient, DrawContext context)
        {
            var rect = context.RenderRect;

            var renderStops = GetRenderStops(gradient);
            var circle = new RadialGradientGeometry(gradient, rect, 1, 1);

            var center = new Point(circle.Center.X / rect.Width, circle.Center.Y / rect.Height);
            var radius = Math.Min(circle.Radius.Width / rect.Width, circle.Radius.Height / rect.Height);

            // TODO: Missing TileMode.Repeat
            // TODO: Missing transform Matrix, only single radius supported

            return new RadialGradientPaint(renderStops, center, radius);
        }
    }
}
