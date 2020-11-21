using MagicGradients;
using Playground.Extensions;
using Playground.ViewModels;
using System.Windows.Input;
using Xamarin.Forms;
using GradientStop = MagicGradients.GradientStop;

namespace Playground.Features.Linear
{
    public class LinearGradientViewModel : ObservableObject
    {
        public ICommand ResetCommand { get; set; }

        public LinearGradientViewModel()
        {
            InitializeGradient();

            ResetCommand = new Command(InitializeGradient);
        }

        private void InitializeGradient()
        {
            var Gradient = new LinearGradient();
            Gradient.Stops.Add(new GradientStop { Color = ColorUtils.GetRandom() });
            Gradient.Stops.Add(new GradientStop { Color = ColorUtils.GetRandom() });
            Gradient.Stops.Add(new GradientStop { Color = ColorUtils.GetRandom() });
            Gradient.Measure(0, 0);
        }
    }
}
