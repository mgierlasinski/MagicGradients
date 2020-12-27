using MagicGradients.Renderers;
using Xamarin.Forms;

namespace MagicGradients.Masks
{
    public class GradientMask : GradientElement
    {
        public static readonly BindableProperty ClipModeProperty = BindableProperty.Create(nameof(ClipMode),
            typeof(ClipMode), typeof(GradientMask), ClipMode.Include);

        public static readonly BindableProperty IsActiveProperty = BindableProperty.Create(nameof(IsActive),
            typeof(bool), typeof(GradientMask), true);

        public ClipMode ClipMode
        {
            get => (ClipMode)GetValue(ClipModeProperty);
            set => SetValue(ClipModeProperty, value);
        }

        public bool IsActive
        {
            get => (bool)GetValue(IsActiveProperty);
            set => SetValue(IsActiveProperty, value);
        }

        public virtual void Clip(RenderContext context)
        {
        }
    }
}
