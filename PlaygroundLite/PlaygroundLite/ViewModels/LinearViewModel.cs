using System.Linq;
using MagicGradients;
using PlaygroundLite.Services;
using Xamarin.Forms;

namespace PlaygroundLite.ViewModels
{
    public class LinearViewModel : GradientViewModel<LinearGradient>
    {
        private double _angle;
        public double Angle
        {
            get => _angle;
            set => SetProperty(ref _angle, value, () => Gradient.Angle = _angle);
        }

        public LinearViewModel()
        {
            InitializeGradient();

            ResetCommand = new Command(InitializeGradient);
        }

        private void InitializeGradient()
        {
            Gradient = new LinearGradient();
            Gradient.Stops.Add(new GradientStop { Color = ColorUtils.GetRandom() });
            Gradient.Stops.Add(new GradientStop { Color = ColorUtils.GetRandom() });
            Gradient.Stops.Add(new GradientStop { Color = ColorUtils.GetRandom() });

            SelectedStop = Gradient.Stops.First();

            UpdateStopsCount();
        }
    }
}
