using MagicGradients.Drawing;
using Microsoft.Maui.Graphics;

namespace MagicGradients.Masks
{
    public class RectangleMaskPainter : MaskPainter, IMaskPainter<IRectangleMask, DrawContext>
    {
        public void Clip(IRectangleMask mask, DrawContext context)
        {
            if (!mask.IsActive)
                return;

            var bounds = mask.Size.GetDrawRectangle(context);

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

        private PointF GetCornerPoint(Dimensions cornerSize, RectangleF bounds, float pixelScaling)
        {
            return new PointF(
                cornerSize.Width.GetDrawPixels(bounds.Width, pixelScaling),
                cornerSize.Height.GetDrawPixels(bounds.Height, pixelScaling));
        }
    }
}
