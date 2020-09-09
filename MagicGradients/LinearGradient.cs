using MagicGradients.Renderers;
using Xamarin.Forms;

namespace MagicGradients
{
    public class LinearGradient : Gradient
    {
        public static readonly BindableProperty AngleProperty = BindableProperty.Create(
            nameof(Angle), typeof(double), typeof(LinearGradient), 0d);

        public double Angle
        {
            get => (double)GetValue(AngleProperty);
            set => SetValue(AngleProperty, value);
        }

        public LinearGradient()
        {
            Shader = new LinearGradientShader(this);
        }
    }
}
