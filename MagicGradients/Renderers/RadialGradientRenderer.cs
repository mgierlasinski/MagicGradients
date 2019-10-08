using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Linq;

namespace MagicGradients.Renderers
{
    public class RadialGradientRenderer
    {
        private readonly RadialGradient _gradient;

        public RadialGradientRenderer(RadialGradient gradient)
        {
            _gradient = gradient;
        }

        public void Render(RenderContext context)
        {
            var info = context.Info;
            var point = _gradient.Center.ToSKPoint();

            var widthIsProportional = IsProportional(RadialGradientFlags.WidthProportional);
            var heightIsProportional = IsProportional(RadialGradientFlags.HeightProportional);
            var xIsProportional = IsProportional(RadialGradientFlags.XProportional);
            var yIsProportional = IsProportional(RadialGradientFlags.YProportional);

            var center = new SKPoint(
                xIsProportional ? info.Width * point.X : point.X,
                yIsProportional ? info.Height * point.Y : point.Y);

            var radiusX = widthIsProportional ? info.Width * _gradient.RadiusX : _gradient.RadiusX;
            var radiusY = heightIsProportional ? info.Height * _gradient.RadiusY : _gradient.RadiusY;
            var radius = Math.Min(radiusX, radiusY);

            var orderedStops = _gradient.Stops.OrderBy(x => x.Offset).ToArray();
            var colors = orderedStops.Select(x => x.Color.ToSKColor()).ToArray();
            var colorPos = orderedStops.Select(x => x.Offset).ToArray();

            var shader = SKShader.CreateRadialGradient(
                center, 
                radius, 
                colors, 
                colorPos,
                SKShaderTileMode.Clamp,
                GetScaleMatrix(center, radiusX, radiusY));

            context.Paint.Shader = shader;
            context.Canvas.DrawRect(info.Rect, context.Paint);
        }

        private SKMatrix GetScaleMatrix(SKPoint center, float radiusX, float radiusY)
        {
            if (radiusX > radiusY)
            {
                return SKMatrix.MakeScale(radiusX / radiusY, 1f, center.X, center.Y);
            }

            if (radiusY > radiusX)
            {
                return SKMatrix.MakeScale(1f, radiusY / radiusX, center.X, center.Y);
            }

            return SKMatrix.MakeIdentity();
        }

        private bool IsProportional(RadialGradientFlags flag) => (_gradient.Flags & flag) != 0;
    }
}
