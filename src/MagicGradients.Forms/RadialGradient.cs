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

        public static readonly BindableProperty RadiusProperty = BindableProperty.Create(
            nameof(RadiusProperty), typeof(Dimensions), typeof(RadialGradient), Dimensions.Zero);

        public Dimensions Radius
        {
            get => (Dimensions)GetValue(RadiusProperty);
            set => SetValue(RadiusProperty, value);
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
