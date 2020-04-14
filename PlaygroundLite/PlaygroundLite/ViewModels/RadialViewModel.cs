using MagicGradients;
using Xamarin.Forms;

namespace PlaygroundLite.ViewModels
{
    public class RadialViewModel : GradientViewModel<RadialGradient>
    {
        public RadialViewModel()
        {
            Gradient = new RadialGradient
            {
                Center = new Point(0.5, 0.5),
                RadiusX = 300,
                RadiusY = 300,
                Flags = RadialGradientFlags.PositionProportional
            };
            Gradient.Stops.Add(new GradientStop { Color = GetRandomColor() });
            Gradient.Stops.Add(new GradientStop { Color = GetRandomColor() });
            Gradient.Stops.Add(new GradientStop { Color = GetRandomColor() });
        }
    }
}
