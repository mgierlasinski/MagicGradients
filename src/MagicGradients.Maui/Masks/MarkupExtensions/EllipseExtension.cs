namespace MagicGradients.Maui.Masks;

[ContentProperty(nameof(Size))]
public class EllipseExtension : MaskExtension, IMarkupExtension<EllipseMask>
{
    public Dimensions Size { get; set; } = Dimensions.Prop(1, 1);

    public EllipseMask ProvideValue(IServiceProvider serviceProvider)
    {
        var mask = new EllipseMask { Size = Size };

        FillValues(mask);

        return mask;
    }

    object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
    {
        return ProvideValue(serviceProvider);
    }
}
