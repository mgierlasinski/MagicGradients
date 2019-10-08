using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Linq;

namespace MagicGradients.Renderers
{
    public class LinearGradientRenderer
    {
        private readonly LinearGradient _gradient;

        public LinearGradientRenderer(LinearGradient gradient)
        {
            _gradient = gradient;
        }

        public void Render(RenderContext context)
        {
            var (startPoint, endPoint) = GetGradientPoints(context.Info.Width, context.Info.Height, _gradient.Angle);

            var orderedStops = _gradient.Stops.OrderBy(x => x.Offset).ToArray();
            var colors = orderedStops.Select(x => x.Color.ToSKColor()).ToArray();
            var colorPos = orderedStops.Select(x => x.Offset).ToArray();

            var shader = SKShader.CreateLinearGradient(
                startPoint, 
                endPoint, 
                colors, 
                colorPos, 
                SKShaderTileMode.Clamp);

            context.Paint.Shader = shader;
            context.Canvas.DrawRect(context.Info.Rect, context.Paint);
        }

        private (SKPoint, SKPoint) GetGradientPoints(int width, int height, double rotation)
        {
            var angle = rotation / 360.0;

            var a = width * Math.Pow(Math.Sin(2 * Math.PI * ((angle + 0.75) / 2)), 2);
            var b = height * Math.Pow(Math.Sin(2 * Math.PI * ((angle + 0.0) / 2)), 2);
            var c = width * Math.Pow(Math.Sin(2 * Math.PI * ((angle + 0.25) / 2)), 2);
            var d = height * Math.Pow(Math.Sin(2 * Math.PI * ((angle + 0.5) / 2)), 2);

            var start = new SKPoint(width - (float)a, (float)b);
            var end = new SKPoint(width - (float)c, (float)d);

            return (start, end);
        }
    }
}
