﻿using Microsoft.Maui.Graphics;

namespace MagicGradients.Maui.Graphics.Drawing
{
    public class LinearGradientPainter : GradientPainter
    {
        public Paint CreatePaint(LinearGradient gradient, DrawContext context)
        {
            var rect = context.RenderRect;

            var renderStops = GetRenderStops(gradient);
            var line = new LinearGradientGeometry(rect, gradient.Angle);
            var startPoint = new Point(line.Start.X / rect.Width, line.Start.Y / rect.Height);
            var endPoint = new Point(line.End.X / rect.Width, line.End.Y / rect.Height);

            // TODO: Missing TileMode.Repeat

            return new LinearGradientPaint(renderStops, startPoint, endPoint);
        }
    }
}
