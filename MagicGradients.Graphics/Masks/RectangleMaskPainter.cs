using MagicGradients.Graphics.Drawing;
using MagicGradients.Masks;
using Microsoft.Maui.Graphics;

namespace MagicGradients.Graphics.Masks
{
    public class RectangleMaskPainter : GradientMaskPainter, IMaskPainter<RectangleMask, DrawContext>
    {
        public void Clip(RectangleMask mask, DrawContext context)
        {
            if (!mask.IsActive)
                return;

            var bounds = GetBounds(mask.Size, context);

            var topLeft = GetCornerPoint(mask.Corners.TopLeft, bounds, context.PixelScaling);
            var topRight = GetCornerPoint(mask.Corners.TopRight, bounds, context.PixelScaling);
            var bottomLeft = GetCornerPoint(mask.Corners.BottomLeft, bounds, context.PixelScaling);
            var bottomRight = GetCornerPoint(mask.Corners.BottomRight, bounds, context.PixelScaling);

            var path = new PathF();
            path.AppendRoundedRectangle(bounds, topLeft.X, topRight.X, bottomLeft.X, bottomRight.X);

            LayoutBounds(mask, bounds, context, false);
            context.Canvas.ClipPath(path);
            RestoreTransform(context.Canvas);
        }

        private PointF GetCornerPoint(Dimensions cornerSize, RectangleF bounds, double pixelScaling)
        {
            return new PointF(
                (float)cornerSize.Width.GetDrawPixels((int)bounds.Width, pixelScaling),
                (float)cornerSize.Height.GetDrawPixels((int)bounds.Height, pixelScaling));
        }
    }
}
