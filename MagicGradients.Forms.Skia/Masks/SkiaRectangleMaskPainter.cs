using MagicGradients.Drawing;
using MagicGradients.Forms.Skia.Drawing;
using MagicGradients.Masks;
using Microsoft.Maui.Graphics.Skia;
using SkiaSharp;

namespace MagicGradients.Forms.Skia.Masks
{
    public class SkiaRectangleMaskPainter : MaskPainter, IMaskPainter<IRectangleMask, DrawContext>
    {
        public void Clip(IRectangleMask mask, DrawContext context)
        {
            if (!mask.IsActive)
                return;

            var roundRect = GetRoundRect(mask, context);
            ClipRoundRect(roundRect, mask, context);
        }

        protected internal void ClipRoundRect(SKRoundRect roundRect, IRectangleMask mask, DrawContext context)
        {
            var canvas = context.Canvas as SkiaCanvasEx;
            if (canvas == null)
                return;

            LayoutBounds(mask, roundRect.Rect.AsRectangleF(), context, false);
            canvas.ClipRoundRect(roundRect, mask.ClipMode.ToSkOperation());
            RestoreTransform(context.Canvas);
        }

        private SKRoundRect GetRoundRect(IRectangleMask mask, DrawContext context)
        {
            var bounds = GetBounds(mask.Size, context).AsSKRect();
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

        private SKPoint GetCornerPoint(Dimensions cornerSize, SKRect bounds, double pixelScaling)
        {
            return new SKPoint(
                (int)cornerSize.Width.GetDrawPixels((int)bounds.Width, pixelScaling),
                (int)cornerSize.Height.GetDrawPixels((int)bounds.Height, pixelScaling));
        }
    }
}
