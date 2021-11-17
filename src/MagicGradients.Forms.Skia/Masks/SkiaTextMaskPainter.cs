using MagicGradients.Drawing;
using MagicGradients.Forms.Skia.Drawing;
using MagicGradients.Masks;
using Microsoft.Maui.Graphics.Skia;
using SkiaSharp;

namespace MagicGradients.Forms.Skia.Masks
{
    public class SkiaTextMaskPainter : IMaskPainter<ITextMask, DrawContext>
    {
        public void Clip(ITextMask mask, DrawContext context)
        {
            if (!mask.IsActive || string.IsNullOrEmpty(mask.Text))
                return;

            using var textPaint = GetTextPaint(mask, context);
            using var textPath = textPaint.GetTextPath(mask.Text, 0, 0);
            textPath.GetTightBounds(out var bounds);

            var canvas = context.GetNativeCanvas<SkiaCanvasEx>();

            using var layout = TextMaskLayout.Create(mask, bounds.AsRectangleF(), context);
            canvas.ClipPath(textPath, mask.ClipMode.ToSkOperation());
        }

        private SKPaint GetTextPaint(ITextMask mask, DrawContext context)
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
    }
}
