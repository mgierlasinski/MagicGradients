using Microsoft.Maui.Graphics;
using System;
using System.Linq;

namespace MagicGradients.Drawing
{
    public class RadialGradientGeometry : GradientGeometry<IRadialGradient>
    {
        public PointF Center { get; private set; }
        public SizeF Radius { get; private set; }
        
        protected override double CalculateRenderOffset(IRadialGradient gradient, double offset, int width, int height)
        {
            var rect = new RectangleF(0, 0, width, height);

            var center = gradient.Center.GetDrawPoint(rect, 1);
            var radius = GetRadius(gradient, center, rect, 1, 1);

            // Use lower dimension (scale = 1) 
            return radius.Width < radius.Height ? offset / radius.Width : offset / Radius.Height;
        }

        public void CalculateGeometry(IRadialGradient gradient, RectangleF rect, float offset, float pixelScaling)
        {
            Center = gradient.Center.GetDrawPoint(rect, pixelScaling);
            Radius = GetRadius(gradient, Center, rect, offset, pixelScaling);
        }
        
        private SizeF GetRadius(IRadialGradient gradient, PointF center, RectangleF rect, float offset, float pixelScaling)
        {
            var radiusX = 0f;
            var radiusY = 0f;

            if (gradient.Shape == RadialGradientShape.Ellipse)
            {
                var distances = GetDistanceInPoints(gradient, center, rect);
                var distanceX = distances.Select(p => Math.Abs(p.X)).Where(x => x > 0);
                var distanceY = distances.Select(p => Math.Abs(p.Y)).Where(y => y > 0);

                // https://github.com/mgierlasinski/MagicGradients/issues/25

                radiusX = gradient.Stretch.IsClosest() ? distanceX.Min() : distanceX.Max();
                radiusY = gradient.Stretch.IsClosest() ? distanceY.Min() : distanceY.Max();
            }

            if (gradient.Shape == RadialGradientShape.Circle)
            {
                var distances = GetEuclideanDistance(gradient, center, rect);
                var distanceXY = gradient.Stretch.IsClosest() ? distances.Min() : distances.Max();

                radiusX = distanceXY;
                radiusY = distanceXY;
            }

            if (gradient.Radius.Width.Value > 0)
            {
                radiusX = gradient.Radius.Width.GetDrawPixels(rect.Width, pixelScaling);
            }

            if (gradient.Radius.Height.Value > 0)
            {
                radiusY = gradient.Radius.Height.GetDrawPixels(rect.Height, pixelScaling);
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

        private PointF[] GetDistanceInPoints(IRadialGradient gradient, PointF center, RectangleF rect)
        {
            var points = gradient.Stretch.IsCorner() ?
                GetCornerPoints(rect) :
                GetSidePoints(center, rect);

            var distances = new PointF[points.Length];

            for (var i = 0; i < distances.Length; i++)
            {
                distances[i] = Subtract(center, points[i]);
            }

            return distances;
        }

        private float[] GetEuclideanDistance(IRadialGradient gradient, PointF center, RectangleF rect)
        {
            var points = gradient.Stretch.IsCorner() ?
                GetCornerPoints(rect) :
                GetSidePoints(center, rect);

            var distances = new float[points.Length];

            for (var i = 0; i < distances.Length; i++)
            {
                distances[i] = center.Distance(points[i]);
            }

            return distances;
        }
        
        private PointF Subtract(PointF pt, PointF sz)
        {
            return new PointF(pt.X - sz.X, pt.Y - sz.Y);
        }
    }
}
