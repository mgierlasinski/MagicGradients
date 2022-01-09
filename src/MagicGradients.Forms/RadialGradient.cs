using BindableProperty = Xamarin.Forms.BindableProperty;

namespace MagicGradients.Forms
{
    public class RadialGradient : Gradient, IRadialGradient
    {
        public static readonly BindableProperty CenterProperty = BindableProperty.Create(
            nameof(Center), typeof(Position), typeof(RadialGradient), Position.Prop(0.5, 0.5));

        public Position Center
        {
            get => (Position)GetValue(CenterProperty);
            set => SetValue(CenterProperty, value);
        }

        public static readonly BindableProperty RadiusXProperty = BindableProperty.Create(
            nameof(RadiusXProperty), typeof(double), typeof(RadialGradient), -1d);

        public double RadiusX
        {
            get => (double)GetValue(RadiusXProperty);
            set => SetValue(RadiusXProperty, value);
        }

        public static readonly BindableProperty RadiusYProperty = BindableProperty.Create(
            nameof(RadiusYProperty), typeof(double), typeof(RadialGradient), -1d);

        public double RadiusY
        {
            get => (double)GetValue(RadiusYProperty);
            set => SetValue(RadiusYProperty, value);
        }

        public static readonly BindableProperty FlagsProperty = BindableProperty.Create(
            nameof(Flags), typeof(RadialGradientFlags), typeof(RadialGradient), RadialGradientFlags.PositionProportional);

        public RadialGradientFlags Flags
        {
            get => (RadialGradientFlags)GetValue(FlagsProperty);
            set => SetValue(FlagsProperty, value);
        }

        public static readonly BindableProperty ShapeProperty = BindableProperty.Create(
            nameof(Shape), typeof(RadialGradientShape), typeof(RadialGradient), RadialGradientShape.Ellipse);

        public RadialGradientShape Shape
        {
            get => (RadialGradientShape)GetValue(ShapeProperty);
            set => SetValue(ShapeProperty, value);
        }

        public static readonly BindableProperty SizeProperty = BindableProperty.Create(
            nameof(Size), typeof(RadialGradientSize), typeof(RadialGradient), RadialGradientSize.FarthestCorner);

        public RadialGradientSize Size
        {
            get => (RadialGradientSize)GetValue(SizeProperty);
            set => SetValue(SizeProperty, value);
        }
    }
}
