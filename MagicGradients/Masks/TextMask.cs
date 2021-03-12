using MagicGradients.Renderers;
using SkiaSharp;
using Xamarin.Forms;

namespace MagicGradients.Masks
{
    public class TextMask : PathMask
    {
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text),
            typeof(string), typeof(TextMask));

        public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(nameof(FontFamily), 
            typeof(string), typeof(TextMask), default(string));

        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize),
            typeof(double), typeof(TextMask), defaultValue: 18d);

        public static readonly BindableProperty FontAttributesProperty = BindableProperty.Create(nameof(FontAttributes), 
            typeof(FontAttributes), typeof(TextMask), FontAttributes.None);

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public string FontFamily
        {
            get => (string)GetValue(FontFamilyProperty);
            set => SetValue(FontFamilyProperty, value);
        }

        [TypeConverter(typeof(FontSizeConverter))]
        public double FontSize
        {
            get => (double)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        public FontAttributes FontAttributes
        {
            get => (FontAttributes)GetValue(FontAttributesProperty);
            set => SetValue(FontAttributesProperty, value);
        }

        public override void Clip(RenderContext context)
        {
            if (!IsActive)
                return;

            using var textPaint = GetTextPaint();
            using var textPath = textPaint.GetTextPath(Text, 0, 0);

            ClipPath(context, textPath);
        }

        private SKPaint GetTextPaint()
        {
            return new SKPaint
            {
                TextSize = (float)FontSize,
                Typeface = SKTypeface.FromFamilyName(FontFamily, FontAttributes switch
                {
                    FontAttributes.Bold => SKFontStyle.Bold,
                    FontAttributes.Italic => SKFontStyle.Italic,
                    _ => SKFontStyle.Normal
                }),
                IsAntialias = true
            };
        }
    }
}