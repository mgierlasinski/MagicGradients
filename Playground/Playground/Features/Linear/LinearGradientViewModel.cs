using MagicGradients;
using Playground.Extensions;
using Playground.ViewModels;
using System.Linq;
using Xamarin.Forms;
using GradientStop = MagicGradients.GradientStop;

namespace Playground.Features.Linear
{
    public class LinearGradientViewModel : GradientViewModel<LinearGradient>
    {
        private double _angle;
        public double Angle
        {
            get => _angle;
            set => SetProperty(ref _angle, value, () => Gradient.Angle = _angle);
        }

        public LinearGradientViewModel()
        {
            InitializeGradient();

            ResetCommand = new Command(InitializeGradient);
        }

        private void InitializeGradient()
        {
            Gradient = new MagicGradients.LinearGradient();
            Gradient.Stops.Add(new GradientStop { Color = ColorUtils.GetRandom() });
            Gradient.Stops.Add(new GradientStop { Color = ColorUtils.GetRandom() });
            Gradient.Stops.Add(new GradientStop { Color = ColorUtils.GetRandom() });

            SelectedStop = Gradient.Stops.First();

            UpdateStopsCount();
        }
    }
}
