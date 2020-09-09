using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Linq;

namespace MagicGradients.Renderers
{
    public class LinearGradientShader : IGradientShader
    {
        private readonly LinearGradient _gradient;

        public LinearGradientShader(LinearGradient gradient)
        {
            _gradient = gradient;
        }

        public SKShader Create(RenderContext context)
        {
            var rect = context.RenderRect;

            var orderedStops = _gradient.Stops.OrderBy(x => x.RenderOffset).ToArray();
            var lastOffset = orderedStops.LastOrDefault()?.RenderOffset ?? 1;

            var colors = orderedStops.Select(x => x.Color.ToSKColor()).ToArray();
            var colorPos = orderedStops.Select(x => x.RenderOffset / lastOffset).ToArray();

            var (startPoint, endPoint) = GetGradientPoints((int)rect.Size.Width, (int)rect.Size.Height, _gradient.Angle, lastOffset);

            var shader = SKShader.CreateLinearGradient(
                startPoint,
                endPoint,
                colors,
                colorPos,
                _gradient.IsRepeating ? SKShaderTileMode.Repeat : SKShaderTileMode.Clamp);

            return shader;
        }

        public double CalculateRenderOffset(double offset, int width, int height)
        {
            // Here the Pythagorean Theorem + Trigonometry is applied
            // to figure out the length of the gradient, which is needed to accurately calculate offset.
            // https://en.wikibooks.org/wiki/Trigonometry/The_Pythagorean_Theorem
            var angleRad = GradientMath.ToRadians(_gradient.Angle);
            var computedLength = Math.Sqrt(Math.Pow(width * Math.Cos(angleRad), 2) + Math.Pow(height * Math.Sin(angleRad), 2));

            return computedLength != 0 ? offset / computedLength : 1;
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
