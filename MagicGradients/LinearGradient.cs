using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Linq;

namespace MagicGradients
{
    public class LinearGradient : Gradient
    {
        public double Angle { get; set; }

        public override SKShader CreateShader(SKPaint paint, SKImageInfo info)
        {
            var (startPoint, endPoint) = GetGradientPoints(info.Width, info.Height, Angle);

            var orderedStops = Stops.OrderBy(x => x.Offset).ToArray();
            var colors = orderedStops.Select(x => x.Color.ToSKColor()).ToArray();
            var colorPos = orderedStops.Select(x => x.Offset).ToArray();
            var tileMode = SKShaderTileMode.Clamp;

            return SKShader.CreateLinearGradient(startPoint, endPoint, colors, colorPos, tileMode);
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

        public override string ToString()
        {
            return $"Angle={Angle}, Stops=LinearGradientStop[{Stops.Count}]";
        }
    }
}
