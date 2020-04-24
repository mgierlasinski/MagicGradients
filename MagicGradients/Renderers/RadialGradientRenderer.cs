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

            var orderedStops = _gradient.Stops.OrderBy(x => x.RenderOffset).ToArray();
            var lastOffset = _gradient.IsRepeating ? orderedStops.LastOrDefault()?.RenderOffset ?? 1 : 1;

            var colors = orderedStops.Select(x => x.Color.ToSKColor()).ToArray();
            var colorPos = orderedStops.Select(x => x.RenderOffset / lastOffset).ToArray();

            var center = GetCenter(info.Width, info.Height);
            var (radiusX, radiusY) = GetRadius(center, info, lastOffset);

            var shader = SKShader.CreateRadialGradient(
                center,
                Math.Min(radiusX, radiusY), 
                colors, 
                colorPos,
                _gradient.IsRepeating ? SKShaderTileMode.Repeat : SKShaderTileMode.Clamp,
                GetScaleMatrix(center, radiusX, radiusY));

            context.Paint.Shader = shader;
            context.Canvas.DrawRect(info.Rect, context.Paint);
        }

        private SKPoint GetCenter(int width, int height)
        {
            var point = _gradient.Center.ToSKPoint();

            var xIsProportional = IsProportional(RadialGradientFlags.XProportional);
            var yIsProportional = IsProportional(RadialGradientFlags.YProportional);

            return new SKPoint(
                xIsProportional ? width * point.X : point.X,
                yIsProportional ? height * point.Y : point.Y);
        }

        private (float, float) GetRadius(SKPoint center, SKImageInfo info, float offset)
        {
            var radiusX = 0f;
            var radiusY = 0f;

            if (_gradient.Shape == RadialGradientShape.Ellipse)
            {
                var distances = GetDistanceInPoints(center, info);
                var distanceX = distances.Select(p => Math.Abs(p.X)).Where(x => x > 0);
                var distanceY = distances.Select(p => Math.Abs(p.Y)).Where(y => y > 0);

                // https://github.com/mgierlasinski/MagicGradients/issues/25

                radiusX = _gradient.Size.IsClosest() ? distanceX.Min() : distanceX.Max();
                radiusY = _gradient.Size.IsClosest() ? distanceY.Min() : distanceY.Max();
            }

            if (_gradient.Shape == RadialGradientShape.Circle)
            {
                var distances = GetEuclideanDistance(center, info);
                var distanceXY = _gradient.Size.IsClosest() ? distances.Min() : distances.Max();

                radiusX = distanceXY;
                radiusY = distanceXY;
            }

            if (_gradient.RadiusX > -1)
            {
                var widthIsProportional = IsProportional(RadialGradientFlags.WidthProportional);
                radiusX = widthIsProportional ? info.Width * (float)_gradient.RadiusX : (float)_gradient.RadiusX;
            }

            if (_gradient.RadiusY > -1)
            {
                var heightIsProportional = IsProportional(RadialGradientFlags.HeightProportional);
                radiusY = heightIsProportional ? info.Height * (float)_gradient.RadiusY : (float)_gradient.RadiusY;
            }
            
            return (radiusX * offset, radiusY * offset);
        }

        private SKPoint[] GetCornerPoints(SKImageInfo info)
        {
            var points = new[]
            {
                new SKPoint(info.Rect.Left, info.Rect.Top),     // leftTop
                new SKPoint(info.Rect.Right, info.Rect.Top),    // rightTop
                new SKPoint(info.Rect.Right, info.Rect.Bottom), // rightBottom
                new SKPoint(info.Rect.Left, info.Rect.Bottom)   // leftBottom
            };

            return points;
        }

        private SKPoint[] GetSidePoints(SKPoint center, SKImageInfo info)
        {
            var points = new[]
            {
                new SKPoint(info.Rect.Left, center.Y),      // left
                new SKPoint(center.X, info.Rect.Top),       // top
                new SKPoint(info.Rect.Right, center.Y),     // right
                new SKPoint(center.X, info.Rect.Bottom)     // bottom
            };

            return points;
        }

        private SKPoint[] GetDistanceInPoints(SKPoint center, SKImageInfo info)
        {
            var points = _gradient.Size.IsCorner() ?
                GetCornerPoints(info) :
                GetSidePoints(center, info);

            var distances = new SKPoint[points.Length];

            for (var i = 0; i < distances.Length; i++)
            {
                distances[i] = center - points[i];
            }

            return distances;
        }

        private float[] GetEuclideanDistance(SKPoint center, SKImageInfo info)
        {
            var points = _gradient.Size.IsCorner() ?
                GetCornerPoints(info) :
                GetSidePoints(center, info);

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

        private bool IsProportional(RadialGradientFlags flag) => (_gradient.Flags & flag) != 0;
    }
}
