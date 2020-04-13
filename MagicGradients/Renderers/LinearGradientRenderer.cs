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
            var computedOffset = GetComputedOffset(orderedStops, info.Width, info.Height);

            var colors = orderedStops.Select(x => x.Color.ToSKColor()).ToArray();
            var colorPos = orderedStops.Select(x => x.RenderOffset / lastOffset).ToArray();

            var (startPoint, endPoint) = GetGradientPoints(info.Width, info.Height, _gradient.Angle, computedOffset);

            var shader = SKShader.CreateLinearGradient(
                startPoint,
                endPoint,
                colors,
                colorPos,
                _gradient.IsRepeating ? SKShaderTileMode.Repeat : SKShaderTileMode.Clamp);

            context.Paint.Shader = shader;
            context.Canvas.DrawRect(context.Info.Rect, context.Paint);
        }

        private float GetComputedOffset(GradientStop[] orderedStops, int width, int height)
        {
            if (!_gradient.IsRepeating || orderedStops.LastOrDefault()?.RenderOffset == null)
                return 1;

            // Here the Pythagorean Theorem + Trigonometry is applied
            // to figure out the length of the gradient, which is needed to accurately calculate the endPoint for the gradient.
            // https://en.wikibooks.org/wiki/Trigonometry/The_Pythagorean_Theorem
            var angleRad = ToRad(_gradient.Angle);
            var computedLength = Math.Sqrt(Math.Pow(width * Math.Cos(angleRad), 2) + Math.Pow(height * Math.Sin(angleRad), 2));

            var lastOffset = orderedStops.LastOrDefault()?.RenderOffset ?? 1;
            return (float)(lastOffset / computedLength);
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

        private double ToRad(double angle)
        {
            return (Math.PI / 180) * angle;
        }
    }
}
