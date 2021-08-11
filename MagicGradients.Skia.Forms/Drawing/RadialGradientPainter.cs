using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Linq;
using static MagicGradients.FlagsHelper;

namespace MagicGradients.Skia.Forms.Drawing
{
    public class RadialGradientPainter : GradientPainter
    {
        public SKShader CreateShader(RadialGradient gradient, DrawContext context)
        {
            var rect = context.RenderRect;

            var renderStops = GetRenderStops(gradient);
            var lastOffset = gradient.IsRepeating ? renderStops.LastOrDefault()?.RenderOffset ?? 1 : 1;

            var colors = renderStops.Select(x => x.Color.ToSKColor()).ToArray();
            var colorPos = renderStops.Select(x => lastOffset > 0 ? x.RenderOffset / lastOffset : 0).ToArray();

            var center = GetCenter(gradient, rect.Size.Width, rect.Size.Height);
            var radius = GetRadius(gradient, center, rect, lastOffset);

            var shader = SKShader.CreateRadialGradient(
                center,
                Math.Min(radius.Width, radius.Height), 
                colors, 
                colorPos,
                gradient.IsRepeating ? SKShaderTileMode.Repeat : SKShaderTileMode.Clamp,
                GetScaleMatrix(center, radius.Width, radius.Height));

            return shader;
        }

        //public double CalculateRenderOffset(double offset, int width, int height)
        //{
        //    var center = GetCenter(width, height);
        //    var radius = GetRadius(center, new SKRect(0, 0, width, height), 1);

        //    // Use lower dimension (scale = 1) 
        //    return radius.Width < radius.Height 
        //        ? offset / radius.Width 
        //        : offset / radius.Height;
        //}
        
        private SKPoint GetCenter(RadialGradient gradient, int width, int height)
        {
            var point = gradient.Center.ToSKPoint();

            var xIsProportional = IsSet(gradient.Flags, RadialGradientFlags.XProportional);
            var yIsProportional = IsSet(gradient.Flags, RadialGradientFlags.YProportional);
            
            return new SKPoint(
                xIsProportional ? width * point.X : point.X,
                yIsProportional ? height * point.Y : point.Y);
        }

        private SKSize GetRadius(RadialGradient gradient, SKPoint center, SKRect rect, float offset)
        {
            var radiusX = 0f;
            var radiusY = 0f;

            if (gradient.Shape == RadialGradientShape.Ellipse)
            {
                var distances = GetDistanceInPoints(gradient, center, rect);
                var distanceX = distances.Select(p => Math.Abs(p.X)).Where(x => x > 0);
                var distanceY = distances.Select(p => Math.Abs(p.Y)).Where(y => y > 0);

                // https://github.com/mgierlasinski/MagicGradients/issues/25

                radiusX = gradient.Size.IsClosest() ? distanceX.Min() : distanceX.Max();
                radiusY = gradient.Size.IsClosest() ? distanceY.Min() : distanceY.Max();
            }

            if (gradient.Shape == RadialGradientShape.Circle)
            {
                var distances = GetEuclideanDistance(gradient, center, rect);
                var distanceXY = gradient.Size.IsClosest() ? distances.Min() : distances.Max();

                radiusX = distanceXY;
                radiusY = distanceXY;
            }

            if (gradient.RadiusX > -1)
            {
                var widthIsProportional = IsSet(gradient.Flags, RadialGradientFlags.WidthProportional);
                radiusX = widthIsProportional ? rect.Width * (float)gradient.RadiusX : (float)gradient.RadiusX;
            }

            if (gradient.RadiusY > -1)
            {
                var heightIsProportional = IsSet(gradient.Flags, RadialGradientFlags.HeightProportional);
                radiusY = heightIsProportional ? rect.Height * (float)gradient.RadiusY : (float)gradient.RadiusY;
            }
            
            return new SKSize(radiusX * offset, radiusY * offset);
        }

        private SKPoint[] GetCornerPoints(SKRect rect)
        {
            var points = new[]
            {
                new SKPoint(rect.Left, rect.Top),     // leftTop
                new SKPoint(rect.Right, rect.Top),    // rightTop
                new SKPoint(rect.Right, rect.Bottom), // rightBottom
                new SKPoint(rect.Left, rect.Bottom)   // leftBottom
            };

            return points;
        }

        private SKPoint[] GetSidePoints(SKPoint center, SKRect rect)
        {
            var points = new[]
            {
                new SKPoint(rect.Left, center.Y),      // left
                new SKPoint(center.X, rect.Top),       // top
                new SKPoint(rect.Right, center.Y),     // right
                new SKPoint(center.X, rect.Bottom)     // bottom
            };

            return points;
        }

        private SKPoint[] GetDistanceInPoints(RadialGradient gradient, SKPoint center, SKRect rect)
        {
            var points = gradient.Size.IsCorner() ?
                GetCornerPoints(rect) :
                GetSidePoints(center, rect);

            var distances = new SKPoint[points.Length];

            for (var i = 0; i < distances.Length; i++)
            {
                distances[i] = center - points[i];
            }

            return distances;
        }

        private float[] GetEuclideanDistance(RadialGradient gradient, SKPoint center, SKRect rect)
        {
            var points = gradient.Size.IsCorner() ?
                GetCornerPoints(rect) :
                GetSidePoints(center, rect);

            var distances = new float[points.Length];

            for (var i = 0; i < distances.Length; i++)
            {
                distances[i] = SKPoint.Distance(center, points[i]);
            }

            return distances;
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
