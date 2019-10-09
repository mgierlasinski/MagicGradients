using MagicGradients.Renderers;
using Xamarin.Forms;

namespace MagicGradients
{
    public class RadialGradient : Gradient
    {
        private readonly RadialGradientRenderer _renderer;

        public static readonly BindableProperty CenterProperty = BindableProperty.Create(
            nameof(Center), typeof(Point), typeof(RadialGradient), default(Point));

        public Point Center
        {
            get => (Point)GetValue(CenterProperty);
            set => SetValue(CenterProperty, value);
        }

        public static readonly BindableProperty RadiusXProperty = BindableProperty.Create(
            nameof(RadiusXProperty), typeof(float), typeof(RadialGradient), -1f);

        public float RadiusX
        {
            get => (float)GetValue(RadiusXProperty);
            set => SetValue(RadiusXProperty, value);
        }

        public static readonly BindableProperty RadiusYProperty = BindableProperty.Create(
            nameof(RadiusYProperty), typeof(float), typeof(RadialGradient), -1f);

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

        public static readonly BindableProperty ShapeProperty = BindableProperty.Create(
            nameof(Shape), typeof(RadialGradientShape), typeof(RadialGradient), RadialGradientShape.Ellipse);

        public RadialGradientShape Shape
        {
            get => (RadialGradientShape)GetValue(ShapeProperty);
            set => SetValue(ShapeProperty, value);
        }

        public static readonly BindableProperty ShapeSizeProperty = BindableProperty.Create(
            nameof(ShapeSize), typeof(RadialGradientShapeSize), typeof(RadialGradient), RadialGradientShapeSize.FarthestCorner);

        public RadialGradientShapeSize ShapeSize
        {
            get => (RadialGradientShapeSize)GetValue(ShapeSizeProperty);
            set => SetValue(ShapeSizeProperty, value);
        }

        public RadialGradient()
        {
            _renderer = new RadialGradientRenderer(this);
        }

        public override void Render(RenderContext context)
        {
            _renderer.Render(context);
        }
    }
}
