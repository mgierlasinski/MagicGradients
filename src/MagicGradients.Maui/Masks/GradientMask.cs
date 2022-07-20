using MagicGradients.Masks;
using Stretch = MagicGradients.Masks.Stretch;

namespace MagicGradients.Forms.Masks;

public class GradientMask : GradientElement, IGradientMask
{
    public static readonly BindableProperty ClipModeProperty = BindableProperty.Create(nameof(ClipMode),
        typeof(ClipMode), typeof(GradientMask), ClipMode.Include);

    public static readonly BindableProperty StretchProperty = BindableProperty.Create(nameof(Stretch),
        typeof(Stretch), typeof(GradientMask), Stretch.None);

    public static readonly BindableProperty IsActiveProperty = BindableProperty.Create(nameof(IsActive),
        typeof(bool), typeof(GradientMask), true);

    public ClipMode ClipMode
    {
        get => (ClipMode)GetValue(ClipModeProperty);
        set => SetValue(ClipModeProperty, value);
    }

    public Stretch Stretch
    {
        get => (Stretch)GetValue(StretchProperty);
        set => SetValue(StretchProperty, value);
    }

    public bool IsActive
    {
        get => (bool)GetValue(IsActiveProperty);
        set => SetValue(IsActiveProperty, value);
    }
}
