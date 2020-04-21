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
            var info = context.Info;

            var orderedStops = _gradient.Stops.OrderBy(x => x.RenderOffset).ToArray();
            var lastOffset = orderedStops.LastOrDefault()?.RenderOffset ?? 1;

            var colors = orderedStops.Select(x => x.Color.ToSKColor()).ToArray();
            var colorPos = orderedStops.Select(x => x.RenderOffset / lastOffset).ToArray();

            var (startPoint, endPoint) = GetGradientPoints(info.Width, info.Height, _gradient.Angle, lastOffset);

            var shader = SKShader.CreateLinearGradient(
                startPoint,
                endPoint,
                colors,
                colorPos,
                _gradient.IsRepeating ? SKShaderTileMode.Repeat : SKShaderTileMode.Clamp);

            context.Paint.Shader = shader;
            context.Canvas.DrawRect(context.Info.Rect, context.Paint);
        }

        private (SKPoint, SKPoint) GetGradientPoints(int width, int height, double rotation, float offset)
        {
            var angle = rotation / 360.0;

            var a = width * Math.Pow(Math.Sin(2 * Math.PI * ((angle + 0.75) / 2)), 2);
            var b = height * Math.Pow(Math.Sin(2 * Math.PI * ((angle + 0.0) / 2)), 2);
            var c = width * Math.Pow(Math.Sin(2 * Math.PI * ((angle + 0.25) / 2)), 2);
            var d = height * Math.Pow(Math.Sin(2 * Math.PI * ((angle + 0.5) / 2)), 2);

            var start = new SKPoint(
                (width - (float)a) * offset, 
                (float)b * offset);

            var end = new SKPoint(
                (width - (float)c) * offset, 
                (float)d * offset);

            return (start, end);
        }
    }
}
