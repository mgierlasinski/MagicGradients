using MagicGradients.Masks;

namespace MagicGradients.Maui.Masks;

public class RectangleMask : GradientMask, IRectangleMask
{
    public static readonly BindableProperty SizeProperty = BindableProperty.Create(nameof(Size),
        typeof(Dimensions), typeof(RectangleMask), Dimensions.Prop(1, 1));

    public static readonly BindableProperty CornersProperty = BindableProperty.Create(nameof(Corners),
        typeof(Corners), typeof(RectangleMask));

    public Dimensions Size
    {
        get => (Dimensions)GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }

    public Corners Corners
    {
        get => (Corners)GetValue(CornersProperty);
        set => SetValue(CornersProperty, value);
    }
}
