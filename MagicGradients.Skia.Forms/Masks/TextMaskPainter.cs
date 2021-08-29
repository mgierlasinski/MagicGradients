using MagicGradients.Masks;
using MagicGradients.Maui.Graphics.Masks;
using MagicGradients.Skia.Forms.Drawing;
using SkiaSharp;
using Xamarin.Forms;

namespace MagicGradients.Skia.Forms.Masks
{
    public class TextMaskPainter : PathMaskPainter, IMaskPainter<TextMask, DrawContext>
    {
        public void Clip(TextMask mask, DrawContext context)
        {
            if (!mask.IsActive || string.IsNullOrEmpty(mask.Text))
                return;

            using var textPaint = GetTextPaint(mask, context);
            using var textPath = textPaint.GetTextPath(mask.Text, 0, 0);

            ClipPath(textPath, mask, context);
        }

        private SKPaint GetTextPaint(TextMask mask, DrawContext context)
        {
            var isBold = (mask.FontAttributes & FontAttributes.Bold) == FontAttributes.Bold;
            var isItalic = (mask.FontAttributes & FontAttributes.Italic) == FontAttributes.Italic;

            var fontStyle = isBold && isItalic ? SKFontStyle.BoldItalic
                : isBold ? SKFontStyle.Bold
                : isItalic ? SKFontStyle.Italic :
                SKFontStyle.Normal;

            return new SKPaint
            {
                TextSize = (float)(mask.FontSize * context.PixelScaling),
                Typeface = SKTypeface.FromFamilyName(mask.FontFamily, fontStyle),
                IsAntialias = true
            };
        }

        protected override void BeginLayout(GradientMask mask, SKRect bounds, DrawContext context)
        {
            var textMask = (TextMask)mask;

            var posX = textMask.HorizontalTextAlignment switch
            {
                TextAlignment.Center => (float)context.RenderRect.Width / 2,
                TextAlignment.End => context.RenderRect.Width,
                _ => 0
            };

            var posY = textMask.VerticalTextAlignment switch
            {
                TextAlignment.Center => (float)context.RenderRect.Height / 2,
                TextAlignment.End => context.RenderRect.Height,
                _ => 0
            };

            context.Canvas.Translate(posX, posY);
        }

        protected override void EndLayout(GradientMask mask, SKRect bounds, DrawContext context)
        {
            var textMask = (TextMask)mask;

            var movX = textMask.HorizontalTextAlignment switch
            {
                TextAlignment.Center => -bounds.MidX,
                TextAlignment.End => -bounds.Right,
                _ => -bounds.Left
            };

            var movY = textMask.VerticalTextAlignment switch
            {
                TextAlignment.Center => -bounds.MidY,
                TextAlignment.End => -bounds.Bottom,
                _ => -bounds.Top
            };

            context.Canvas.Translate(movX, movY);
        }
    }
}
