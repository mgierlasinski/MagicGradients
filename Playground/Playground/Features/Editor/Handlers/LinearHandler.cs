using MagicGradients;
using Playground.Extensions;
using Playground.ViewModels;

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

        public LinearHandler(GradientEditorViewModel parent)
        {
            _parent = parent;
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
