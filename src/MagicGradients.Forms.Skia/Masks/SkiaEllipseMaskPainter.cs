using MagicGradients.Drawing;
using MagicGradients.Masks;
using Microsoft.Maui.Graphics.Skia;
using SkiaSharp;

namespace MagicGradients.Forms.Skia.Masks
{
    public class SkiaEllipseMaskPainter : SkiaRectangleMaskPainter, IMaskPainter<IEllipseMask, DrawContext>
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
            var bounds = mask.Size.GetDrawRectangle(context).AsSKRect();
            return new SKRoundRect(bounds, bounds.Width / 2, bounds.Height / 2);
        }
    }
}
