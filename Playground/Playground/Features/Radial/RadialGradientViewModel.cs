using MagicGradients;
using Playground.Extensions;
using Playground.ViewModels;
using System.Windows.Input;
using Xamarin.Forms;
using GradientStop = MagicGradients.GradientStop;

namespace Playground.Features.Radial
{
    public class RadialGradientViewModel : ObservableObject
    {
        public ICommand ResetCommand { get; set; }

        public RadialGradientViewModel()
        {
            InitializeGradient();

            ResetCommand = new Command(InitializeGradient);
        }

        private void InitializeGradient()
        {
            var Gradient = new RadialGradient
            {
                Flags = RadialGradientFlags.PositionProportional
            };

            Gradient.Stops.Add(new GradientStop { Color = ColorUtils.GetRandom() });
            Gradient.Stops.Add(new GradientStop { Color = ColorUtils.GetRandom() });
            Gradient.Stops.Add(new GradientStop { Color = ColorUtils.GetRandom() });
            Gradient.Measure(0, 0);
        }
    }
}
