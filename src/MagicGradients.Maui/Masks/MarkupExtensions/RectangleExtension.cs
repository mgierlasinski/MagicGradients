namespace MagicGradients.Maui.Masks;

[ContentProperty(nameof(Size))]
public class RectangleExtension : MaskExtension, IMarkupExtension<RectangleMask>
{
    public Dimensions Size { get; set; } = Dimensions.Prop(1, 1);
    public Corners Corners { get; set; }

    public RectangleMask ProvideValue(IServiceProvider serviceProvider)
    {
        var mask = new RectangleMask
        {
            Size = Size,
            Corners = Corners
        };

        FillValues(mask);

        return mask;
    }

    object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
    {
        return ProvideValue(serviceProvider);
    }
}
