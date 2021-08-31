using MagicGradients.Masks;
using MagicGradients.Maui.Graphics.Masks;
using MagicGradients.Skia.Forms.Drawing;
using SkiaSharp;

namespace MagicGradients.Skia.Forms.Masks
{
    public class EllipseMaskPainter : RectangleMaskPainter, IMaskPainter<EllipseMask, DrawContext>
    {
        public void Clip(EllipseMask mask, DrawContext context)
        {
            if (!mask.IsActive)
                return;

            var ellipse = GetEllipse(mask, context);
            ClipRoundRect(ellipse, mask, context);
        }

        private SKRoundRect GetEllipse(EllipseMask mask, DrawContext context)
        {
            var bounds = GetBounds(mask.Size, context);
            return new SKRoundRect(bounds, (float)bounds.Width / 2, (float)bounds.Height / 2);
        }
    }
}
