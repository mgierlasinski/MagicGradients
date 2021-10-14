using MagicGradients.Drawing;
using SkiaSharp;
using System;
using System.Linq;

namespace MagicGradients.Forms.SkiaViews.Drawing
{
    public class RadialGradientPainter : GradientPainter
    {
        public SKShader CreateShader(RadialGradient gradient, DrawContext context)
        {
            var rect = context.RenderRect.ToRectF();

            var circle = new RadialGradientGeometry();
            circle.CalculateOffsets(gradient, context.RenderRect.Width, context.RenderRect.Height);

            var renderStops = GetRenderStops(gradient);
            var lastOffset = gradient.IsRepeating ? renderStops.LastOrDefault()?.RenderOffset ?? 1 : 1;
            var colors = renderStops.Select(x => x.Color.ToSKColor()).ToArray();
            var colorPos = renderStops.Select(x => lastOffset > 0 ? x.RenderOffset / lastOffset : 0).ToArray();

            circle.CalculateGeometry(gradient, rect, lastOffset, context.PixelScaling);

            var center = circle.Center.ToSKPoint();

            var shader = SKShader.CreateRadialGradient(
                center,
                Math.Min(circle.Radius.Width, circle.Radius.Height), 
                colors, 
                colorPos,
                gradient.IsRepeating ? SKShaderTileMode.Repeat : SKShaderTileMode.Clamp,
                GetScaleMatrix(center, circle.Radius.Width, circle.Radius.Height));

            return shader;
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
    }
}
