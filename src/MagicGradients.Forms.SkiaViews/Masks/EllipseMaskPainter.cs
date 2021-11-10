using MagicGradients.Drawing;
using MagicGradients.Masks;
using SkiaSharp;
using DrawContext = MagicGradients.Forms.SkiaViews.Drawing.DrawContext;

namespace MagicGradients.Forms.SkiaViews.Masks
{
    public class EllipseMaskPainter : RectangleMaskPainter, IMaskPainter<IEllipseMask, DrawContext>
    {
        public void Clip(IEllipseMask mask, DrawContext context)
        {
            if (!mask.IsActive)
                return;

            var ellipse = GetEllipse(mask, context);
            ClipRoundRect(ellipse, mask, context);
        }

        private SKRoundRect GetEllipse(IEllipseMask mask, DrawContext context)
        {
            var bounds = GetBounds(mask.Size, context);
            return new SKRoundRect(bounds, (float)bounds.Width / 2, (float)bounds.Height / 2);
        }
    }
}
