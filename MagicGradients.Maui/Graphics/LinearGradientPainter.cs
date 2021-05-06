using Microsoft.Maui.Graphics;

namespace MagicGradients.Maui.Graphics
{
    public class LinearGradientPainter : GradientPainter
    {
        public Paint CreatePaint(LinearGradient gradient, DrawContext context)
        {
            var rect = context.RenderRect;

            var renderStops = GetRenderStops(gradient);
            var line = new GradientLine(rect, gradient.Angle);
            var startPoint = new Point(line.Start.X / rect.Width, line.Start.Y / rect.Height);
            var endPoint = new Point(line.End.X / rect.Width, line.End.Y / rect.Height);

            return new LinearGradientPaint(renderStops, startPoint, endPoint);
        }
    }
}
