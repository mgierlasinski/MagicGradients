using MagicGradients.Renderers;
using Xamarin.Forms;

namespace MagicGradients
{
    public class LinearGradient : Gradient
    {
        private readonly LinearGradientRenderer _renderer;

        public static readonly BindableProperty AngleProperty = BindableProperty.Create(
            nameof(Angle), typeof(double), typeof(LinearGradient), 0d);

        public double Angle
        {
            get => (double)GetValue(AngleProperty);
            set => SetValue(AngleProperty, value);
        }

        public static readonly BindableProperty RepeatingProperty = BindableProperty.Create(
            nameof(Repeating), typeof(bool), typeof(LinearGradient), false);

        public bool Repeating
        {
            get => (bool)GetValue(RepeatingProperty);
            set => SetValue(RepeatingProperty, value);
        }

        public LinearGradient()
        {
            _renderer = new LinearGradientRenderer(this);
        }

        public override void Render(RenderContext context)
        {
            _renderer.Render(context);
        }

        public override string ToString()
        {
            return $"Angle={Angle}, Stops=LinearGradientStop[{Stops.Count}]";
        }
    }
}
