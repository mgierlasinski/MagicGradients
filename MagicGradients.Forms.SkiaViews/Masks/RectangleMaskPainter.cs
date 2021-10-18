using MagicGradients.Drawing;
using MagicGradients.Masks;
using SkiaSharp;
using DrawContext = MagicGradients.Forms.SkiaViews.Drawing.DrawContext;

namespace MagicGradients.Forms.SkiaViews.Masks
{
    public class RectangleMaskPainter : GradientMaskPainter, IMaskPainter<IRectangleMask, DrawContext>
    {
        public void Clip(IRectangleMask mask, DrawContext context)
        {
            if (!mask.IsActive)
                return;

            var roundRect = GetRoundRect(mask, context);
            ClipRoundRect(roundRect, mask, context);
        }

        protected internal void ClipRoundRect(SKRoundRect roundRect, IGradientMask mask, DrawContext context)
        {
            using var canvasLock = new CanvasLock(context.Canvas);

            LayoutBounds(mask, roundRect.Rect, context, false);
            context.Canvas.ClipRoundRect(roundRect, mask.ClipMode.ToSkOperation(), true);
        }

        private SKRoundRect GetRoundRect(IRectangleMask mask, DrawContext context)
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
