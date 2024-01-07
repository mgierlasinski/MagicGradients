using System.ComponentModel;

namespace MagicGradients.Maui.Masks;

[ContentProperty(nameof(Text))]
public class TextExtension : MaskExtension, IMarkupExtension<TextMask>
{
    public string Text { get; set; }
    public string FontFamily { get; set; }
    public FontAttributes FontAttributes { get; set; }
    public TextAlignment HorizontalTextAlignment { get; set; }
    public TextAlignment VerticalTextAlignment { get; set; }

    [TypeConverter(typeof(FontSizeConverter))]
    public double FontSize { get; set; } = TextMask.DefaultFontSize;

    public TextMask ProvideValue(IServiceProvider serviceProvider)
    {
        var mask = new TextMask
        {
            Text = Text,
            FontFamily = FontFamily,
            FontSize = FontSize,
            FontAttributes = FontAttributes,
            HorizontalTextAlignment = HorizontalTextAlignment,
            VerticalTextAlignment = VerticalTextAlignment
        };

        FillValues(mask);

        return mask;
    }

    object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
    {
        return ProvideValue(serviceProvider);
    }
}
