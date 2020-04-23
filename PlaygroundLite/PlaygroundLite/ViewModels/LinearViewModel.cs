using MagicGradients;
using PlaygroundLite.Services;

namespace PlaygroundLite.ViewModels
{
    public class LinearViewModel : GradientViewModel<LinearGradient>
    {
        private double _angle;
        public double Angle
        {
            get => _angle;
            set => SetProperty(ref _angle, value, onChanged: () => Gradient.Angle = _angle);
        }

        public LinearViewModel()
        {
            Gradient = new LinearGradient();
            Gradient.Stops.Add(new GradientStop { Color = ColorUtils.GetRandom() });
            Gradient.Stops.Add(new GradientStop { Color = ColorUtils.GetRandom() });
            Gradient.Stops.Add(new GradientStop { Color = ColorUtils.GetRandom() });
        }
    }
}
