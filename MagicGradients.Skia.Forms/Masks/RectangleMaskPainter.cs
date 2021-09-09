using MagicGradients.Graphics.Masks;
using MagicGradients.Masks;
using MagicGradients.Skia.Forms.Drawing;
using SkiaSharp;

namespace MagicGradients.Skia.Forms.Masks
{
    public class RectangleMaskPainter : GradientMaskPainter, IMaskPainter<RectangleMask, DrawContext>
    {
        public void Clip(RectangleMask mask, DrawContext context)
        {
            if (!mask.IsActive)
                return;

            var roundRect = GetRoundRect(mask, context);
            ClipRoundRect(roundRect, mask, context);
        }

        protected internal void ClipRoundRect(SKRoundRect roundRect, GradientMask mask, DrawContext context)
        {
            using var canvasLock = new CanvasLock(context.Canvas);

            LayoutBounds(mask, roundRect.Rect, context, false);
            context.Canvas.ClipRoundRect(roundRect, mask.ClipMode.ToSkOperation(), true);
        }

        private SKRoundRect GetRoundRect(RectangleMask mask, DrawContext context)
        {
            var bounds = GetBounds(mask.Size, context);
            var roundRect = new SKRoundRect();

            roundRect.SetRectRadii(bounds, new[]
            {
                GetCornerPoint(mask.Corners.TopLeft, bounds, context.PixelScaling),
                GetCornerPoint(mask.Corners.TopRight, bounds, context.PixelScaling),
                GetCornerPoint(mask.Corners.BottomRight, bounds, context.PixelScaling),
                GetCornerPoint(mask.Corners.BottomLeft, bounds, context.PixelScaling)
            });

            return roundRect;
        }

        private SKPoint GetCornerPoint(Dimensions cornerSize, SKRectI bounds, double pixelScaling)
        {
            return new SKPoint(
                (int)cornerSize.Width.GetDrawPixels(bounds.Width, pixelScaling),
                (int)cornerSize.Height.GetDrawPixels(bounds.Height, pixelScaling));
        }
    }
}
