using MagicGradients.Renderers;
using Xamarin.Forms;

namespace MagicGradients.Masks
{
    public class GradientMask : GradientElement
    {
        public static readonly BindableProperty ClipModeProperty = BindableProperty.Create(nameof(ClipMode),
            typeof(ClipMode), typeof(GradientMask), ClipMode.Intersect);

        public ClipMode ClipMode
        {
            get => (ClipMode)GetValue(ClipModeProperty);
            set => SetValue(ClipModeProperty, value);
        }

        public virtual void Clip(RenderContext context)
        {
        }
    }
}
