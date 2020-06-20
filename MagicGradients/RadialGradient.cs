using MagicGradients.Renderers;
using Xamarin.Forms;

namespace MagicGradients
{
    public class RadialGradient : Gradient
    {
        private readonly RadialGradientRenderer _renderer;

        public static readonly BindableProperty CenterProperty = BindableProperty.Create(
            nameof(Center), typeof(Point), typeof(RadialGradient), new Point(0.5, 0.5));

        public Point Center
        {
            get => (Point)GetValue(CenterProperty);
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

        public RadialGradient()
        {
            _renderer = new RadialGradientRenderer(this);
        }

        public override void Render(RenderContext context)
        {
#if DEBUG_RENDER
            System.Diagnostics.Debug.WriteLine($"Rendering Radial Gradient with {Stops.Count} stops");
#endif
            _renderer.Render(context);
        }

        protected override double CalculateRenderOffset(double offset, int width, int height)
        {
            return offset;
        }
    }
}
