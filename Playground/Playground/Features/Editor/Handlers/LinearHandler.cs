using MagicGradients;
using Playground.Extensions;

namespace Playground.Features.Editor.Handlers
{
    public class LinearHandler
    {
        private readonly GradientEditorViewModel _parent;

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
