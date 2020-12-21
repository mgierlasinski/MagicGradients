using MagicGradients.Renderers;
using SkiaSharp;
using Xamarin.Forms;

namespace MagicGradients.Masks
{
    public class TextMask : PathMask
    {
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text),
            typeof(string), typeof(TextMask), defaultValue: "Hello world");

        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize),
            typeof(double), typeof(TextMask), defaultValue: 100d);

        
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public double FontSize
        {
            get => (double)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        public override void Clip(RenderContext context)
        {
            using (var textPaint = new SKPaint())
            {
                textPaint.TextSize = (float)FontSize;
                textPaint.IsAntialias = true;

                using (var textPath = textPaint.GetTextPath(Text, 0, 0))
                {
                    ClipPath(context, textPath);
                }
            }
        }

        public override string ToString() => "Text Mask";
    }
}