namespace MagicGradients.Maui.Masks;

[ContentProperty(nameof(Data))]
public class PathExtension : MaskExtension, IMarkupExtension<PathMask>
{
    public string Data { get; set; }

    public PathMask ProvideValue(IServiceProvider serviceProvider)
    {
        var mask = new PathMask { Data = Data };
        
        FillValues(mask);

        return mask;
    }

    object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
    {
        return ProvideValue(serviceProvider);
    }
}
