using System.Windows.Input;
using MagicGradients;
using Playground.Extensions;
using Playground.ViewModels;
using Xamarin.Forms;
using GradientStop = MagicGradients.GradientStop;

namespace Playground.Features.Editor.Handlers
{
    public class LinearHandler : ObservableObject
    {
        private readonly GradientEditorViewModel _parent;

        private bool _useLegacyShader;
        public bool UseLegacyShader
        {
            get => _useLegacyShader;
            set => SetProperty(ref _useLegacyShader, value);
        }

        public ICommand RotateCommand { get; }

        public LinearHandler(GradientEditorViewModel parent)
        {
            _parent = parent;

            RotateCommand = new Command<string>((x) =>
            {
                if (_parent.Gradient is LinearGradient linear)
                    linear.Angle = double.Parse(x);
            });
        }

        public Gradient Create()
        {
            var linear = new LinearGradient();
            linear.Stops.Add(new GradientStop { Color = ColorUtils.GetRandom() });
            linear.Stops.Add(new GradientStop { Color = ColorUtils.GetRandom() });
            linear.Stops.Add(new GradientStop { Color = ColorUtils.GetRandom() });
            linear.Measure(0, 0);

            return linear;
        }
    }
}
