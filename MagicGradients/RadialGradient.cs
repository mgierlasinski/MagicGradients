using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Linq;
using Xamarin.Forms;

namespace MagicGradients
{
    public class RadialGradient : Gradient
    {
        public static readonly BindableProperty CenterProperty = BindableProperty.Create(
            nameof(Center), typeof(Point), typeof(RadialGradient), default(Point));

        public Point Center
        {
            get => (Point)GetValue(CenterProperty);
            set => SetValue(CenterProperty, value);
        }

        public static readonly BindableProperty RadiusXProperty = BindableProperty.Create(
            nameof(RadiusXProperty), typeof(float), typeof(RadialGradient), 0f);

        public float RadiusX
        {
            get => (float)GetValue(RadiusXProperty);
            set => SetValue(RadiusXProperty, value);
        }

        public static readonly BindableProperty RadiusYProperty = BindableProperty.Create(
            nameof(RadiusYProperty), typeof(float), typeof(RadialGradient), 0f);

        public float RadiusY
        {
            get => (float)GetValue(RadiusYProperty);
            set => SetValue(RadiusYProperty, value);
        }

        public static readonly BindableProperty FlagsProperty = BindableProperty.Create(
            nameof(Flags), typeof(RadialGradientFlags), typeof(RadialGradient), RadialGradientFlags.PositionProportional);

        public RadialGradientFlags Flags
        {
            get => (RadialGradientFlags)GetValue(FlagsProperty);
            set => SetValue(FlagsProperty, value);
        }

        public override SKShader CreateShader(SKPaint paint, SKImageInfo info)
        {
            var widthIsProportional = (Flags & RadialGradientFlags.WidthProportional) != 0;
            var heightIsProportional = (Flags & RadialGradientFlags.HeightProportional) != 0;
            var xIsProportional = (Flags & RadialGradientFlags.XProportional) != 0;
            var yIsProportional = (Flags & RadialGradientFlags.YProportional) != 0;

            var point = Center.ToSKPoint();

            var center = new SKPoint(
                xIsProportional ? info.Width * point.X : point.X,
                yIsProportional ? info.Height * point.Y : point.Y);

            var radiusX = widthIsProportional ? info.Width * RadiusX : RadiusX;
            var radiusY = heightIsProportional ? info.Height * RadiusY : RadiusY;
            var radius = Math.Min(radiusX, radiusY);

            var orderedStops = Stops.OrderBy(x => x.Offset).ToArray();
            var colors = orderedStops.Select(x => x.Color.ToSKColor()).ToArray();
            var colorPos = orderedStops.Select(x => x.Offset).ToArray();
            var tileMode = SKShaderTileMode.Clamp;
            var scaleMatrix = GetScaleMatrix(center, radiusX, radiusY);

            return SKShader.CreateRadialGradient(center, radius, colors, colorPos, tileMode, scaleMatrix);
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
