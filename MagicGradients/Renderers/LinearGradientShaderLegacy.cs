using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Linq;

namespace MagicGradients.Renderers
{
    public class LinearGradientShaderLegacy : IGradientShader
    {
        private readonly LinearGradient _gradient;

        public LinearGradientShaderLegacy(LinearGradient gradient)
        {
            _gradient = gradient;
        }

        public SKShader Create(RenderContext context)
        {
            var rect = context.RenderRect;

            var renderStops = GetRenderStops();
            var lastOffset = renderStops.LastOrDefault()?.RenderOffset ?? 1;

            var colors = renderStops.Select(x => x.Color.ToSKColor()).ToArray();
            var colorPos = renderStops.Select(x => lastOffset > 0 ? x.RenderOffset / lastOffset : 0).ToArray();

            var (startPoint, endPoint) = GetGradientPoints(rect.Size.Width, rect.Size.Height, _gradient.Angle, lastOffset);

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

        private GradientStop[] GetRenderStops()
        {
            // SkiaSharp needs at least two stops to render single color
            if (_gradient.Stops.Count == 1)
            {
                return new[]
                {
                    new GradientStop { RenderOffset = 0, Color = _gradient.Stops[0].Color },
                    new GradientStop { RenderOffset = 1, Color = _gradient.Stops[0].Color }
                };
            }
            
            return _gradient.Stops.OrderBy(x => x.RenderOffset).ToArray();
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
