using Microsoft.Maui.Graphics;
using static MagicGradients.FlagsHelper;

namespace MagicGradients.Maui.Graphics.Drawing
{
    public class RadialGradientPainter : GradientPainter
    {
        public Paint CreatePaint(RadialGradient gradient, DrawContext context)
        {
            var rect = context.RenderRect;

            var renderStops = GetRenderStops(gradient);
            var center = GetCenter(gradient, rect);

            return new RadialGradientPaint(renderStops, center, 0.5);
        }

        private Point GetCenter(RadialGradient gradient, RectangleF rect)
        {
            var point = gradient.Center;

            var xIsProportional = IsSet(gradient.Flags, RadialGradientFlags.XProportional);
            var yIsProportional = IsSet(gradient.Flags, RadialGradientFlags.YProportional);

            return new Point(
                xIsProportional ? point.X : point.X / rect.Width,
                yIsProportional ? point.Y : point.Y / rect.Height);
        }
    }
}
