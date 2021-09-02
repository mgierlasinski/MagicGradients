using System;
using System.Linq;
using Microsoft.Maui.Graphics;
using static MagicGradients.FlagsHelper;

namespace MagicGradients.Graphics.Drawing
{
    public class RadialGradientGeometry : GradientGeometry<RadialGradient>
    {
        public PointF Center { get; private set; }
        public SizeF Radius { get; private set; }
        
        protected override double CalculateRenderOffset(RadialGradient gradient, double offset, int width, int height)
        {
            var rect = new RectangleF(0, 0, width, height);

            var center = GetCenter(gradient, rect, 1);
            var radius = GetRadius(gradient, center, rect, 1, 1);

            // Use lower dimension (scale = 1) 
            return radius.Width < radius.Height ? offset / radius.Width : offset / Radius.Height;
        }

        public void CalculateGeometry(RadialGradient gradient, RectangleF rect, float offset, float pixelScaling)
        {
            Center = GetCenter(gradient, rect, pixelScaling);
            Radius = GetRadius(gradient, Center, rect, offset, pixelScaling);
        }

        private PointF GetCenter(RadialGradient gradient, RectangleF rect, float pixelScaling)
        {
            var point = gradient.Center;

            var xIsProportional = IsSet(gradient.Flags, RadialGradientFlags.XProportional);
            var yIsProportional = IsSet(gradient.Flags, RadialGradientFlags.YProportional);
            
            return new PointF(
                (float)(xIsProportional ? rect.Width * point.X : point.X * pixelScaling),
                (float)(yIsProportional ? rect.Height * point.Y : point.Y * pixelScaling));
        }
        
        private SizeF GetRadius(RadialGradient gradient, PointF center, RectangleF rect, float offset, float pixelScaling)
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
                radiusX = widthIsProportional ? rect.Width * (float)gradient.RadiusX : (float)gradient.RadiusX * pixelScaling;
            }

            if (gradient.RadiusY > -1)
            {
                var heightIsProportional = IsSet(gradient.Flags, RadialGradientFlags.HeightProportional);
                radiusY = heightIsProportional ? rect.Height * (float)gradient.RadiusY : (float)gradient.RadiusY * pixelScaling;
            }

            return new SizeF(radiusX * offset, radiusY * offset);
        }

        private PointF[] GetCornerPoints(RectangleF rect)
        {
            var points = new[]
            {
                new PointF(rect.Left, rect.Top),     // leftTop
                new PointF(rect.Right, rect.Top),    // rightTop
                new PointF(rect.Right, rect.Bottom), // rightBottom
                new PointF(rect.Left, rect.Bottom)   // leftBottom
            };

            return points;
        }

        private PointF[] GetSidePoints(PointF center, RectangleF rect)
        {
            var points = new[]
            {
                new PointF(rect.Left, center.Y),      // left
                new PointF(center.X, rect.Top),       // top
                new PointF(rect.Right, center.Y),     // right
                new PointF(center.X, rect.Bottom)     // bottom
            };

            return points;
        }

        private PointF[] GetDistanceInPoints(RadialGradient gradient, PointF center, RectangleF rect)
        {
            var points = gradient.Size.IsCorner() ?
                GetCornerPoints(rect) :
                GetSidePoints(center, rect);

            var distances = new PointF[points.Length];

            for (var i = 0; i < distances.Length; i++)
            {
                distances[i] = Subtract(center, points[i]);
            }

            return distances;
        }

        private float[] GetEuclideanDistance(RadialGradient gradient, PointF center, RectangleF rect)
        {
            var points = gradient.Size.IsCorner() ?
                GetCornerPoints(rect) :
                GetSidePoints(center, rect);

            var distances = new float[points.Length];

            for (var i = 0; i < distances.Length; i++)
            {
                distances[i] = Distance(center, points[i]);
            }

            return distances;
        }
        
        private PointF Subtract(PointF pt, PointF sz)
        {
            return new PointF(pt.X - sz.X, pt.Y - sz.Y);
        }

        private float Distance(PointF point, PointF other)
        {
            var dx = point.X - other.X;
            var dy = point.X - other.X;
            var ls = dx * dx + dy * dy;

            return (float)Math.Sqrt(ls);
        }
    }
}
