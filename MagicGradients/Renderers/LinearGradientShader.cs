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

            var renderStops = GetRenderStops();
            var colors = renderStops.Select(x => x.Color.ToSKColor()).ToArray();
            var colorPos = renderStops.Select(x => x.RenderOffset).ToArray();

            var line = GetGradientLine(rect, _gradient.Angle);
            var startPoint = line.Start;
            var endPoint = line.End;

            if (_gradient.IsRepeating)
            {
                var firstOffset = renderStops.FirstOrDefault()?.RenderOffset ?? 0;
                var lastOffset = renderStops.LastOrDefault()?.RenderOffset ?? 1;

                startPoint = GetColorPoint(line, firstOffset);
                endPoint = GetColorPoint(line, lastOffset);

                for (var i = 0; i < colorPos.Length; i++)
                {
                    colorPos[i] = ScaleWithBias(colorPos[i], firstOffset, lastOffset, 0, 1);
                }
            }

            var shader = SKShader.CreateLinearGradient(
                startPoint,
                endPoint,
                colors,
                colorPos,
                _gradient.IsRepeating ? SKShaderTileMode.Repeat : SKShaderTileMode.Clamp);

            return shader;
        }
        
        private float ScaleWithBias(float input, float inLow, float inHigh, float outLow, float outHigh)
        {
            // Calculation
            // https://gamedev.stackexchange.com/questions/33441/how-to-convert-a-number-from-one-min-max-set-to-another-min-max-set
            return (input - inLow) / (inHigh - inLow) * (outHigh - outLow) + outLow;
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

        
        private GradientLine GetGradientLine(SKRectI boxBounds, double angleDegrees)
        {
            // Calculation
            // https://medium.com/@patrickbrosset/do-you-really-understand-css-linear-gradients-631d9a895caf

            var angleRadians = GradientMath.ToRadians(GradientMath.FromDegrees(angleDegrees));

            var lineLength =
                Math.Abs(boxBounds.Width * Math.Sin(angleRadians)) +
                Math.Abs(boxBounds.Height * Math.Cos(angleRadians));

            var center = new SKPoint(boxBounds.MidX, boxBounds.MidY);

            var yDiff = (float)(Math.Sin(angleRadians - Math.PI / 2) * lineLength / 2);
            var xDiff = (float)(Math.Cos(angleRadians - Math.PI / 2) * lineLength / 2);

            return new GradientLine
            {
                Start = new SKPoint(
                    center.X - xDiff, 
                    center.Y - yDiff),
                End = new SKPoint(
                    center.X + xDiff, 
                    center.Y + yDiff),
                Length = lineLength,
                Angle = angleRadians
            };
        }

        private SKPoint GetColorPoint(GradientLine gradientLine, float position)
        {
            var angle = gradientLine.Angle;

            var yDiff = Math.Sin(angle - Math.PI / 2) * 
                        (gradientLine.Length * position);
            var xDiff = Math.Cos(angle - Math.PI / 2) *
                        (gradientLine.Length * position);

            return new SKPoint(
                gradientLine.Start.X + (float)xDiff, 
                gradientLine.Start.Y + (float)yDiff);
        }
    }

    public class GradientLine
    {
        public SKPoint Start { get; set; }
        public SKPoint End { get; set; }
        public double Length { get; set; }
        public double Angle { get; set; }
    }
}
