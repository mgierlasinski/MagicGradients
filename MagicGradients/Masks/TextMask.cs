using MagicGradients.Renderers;
using SkiaSharp;
using Xamarin.Forms;

namespace MagicGradients.Masks
{
    public enum TextMaskFill
    {
        Center, CenterAndScale, Fill
    }

    public class TextMask : GradientElement, IMask
    {
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text),
            typeof(string), typeof(TextMask), defaultValue: "Hello world");

        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize),
            typeof(double), typeof(TextMask), defaultValue: 100d);

        public static readonly BindableProperty FillProperty = BindableProperty.Create(nameof(Fill),
            typeof(TextMaskFill), typeof(TextMask), defaultValue: TextMaskFill.Center);

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

        public TextMaskFill Fill
        {
            get => (TextMaskFill)GetValue(FillProperty);
            set => SetValue(FillProperty, value);
        }

        public void Clip(RenderContext context)
        {
            using (var paint = new SKPaint())
            {
                paint.TextSize = (float)FontSize;
                paint.IsAntialias = true;

                ClipText(context, paint);
            }
        }

        private void ClipText(RenderContext context, SKPaint textPaint)
        {
            using (new CanvasLock(context.Canvas))
            using (var textPath = textPaint.GetTextPath(Text, 0, 0))
            {
                textPath.GetTightBounds(out var bounds);

                context.Canvas.Translate((float)context.RenderRect.Width / 2, (float)context.RenderRect.Height / 2);

                if (Fill == TextMaskFill.CenterAndScale)
                    context.Canvas.Scale((float)context.RenderRect.Width / context.CanvasRect.Width, (float)context.RenderRect.Height / context.CanvasRect.Height);

                if (Fill == TextMaskFill.Fill)
                    context.Canvas.Scale(context.RenderRect.Width / bounds.Width, context.RenderRect.Height / bounds.Height);

                context.Canvas.Translate(-bounds.MidX, -bounds.MidY);
                context.Canvas.ClipPath(textPath);
            }
        }

        public override string ToString()
        {
            return "Text Mask";
        }
    }
}