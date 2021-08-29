using MagicGradients.Masks;
using MagicGradients.Maui.Graphics.Drawing;
using Microsoft.Maui.Graphics;

namespace MagicGradients.Maui.Graphics.Masks
{
    public class RectangleMaskPainter : GradientMaskPainter, IMaskPainter<RectangleMask, DrawContext>
    {
        public void Clip(RectangleMask mask, DrawContext context)
        {
            if (!mask.IsActive)
                return;

            var bounds = GetSizeRect(context, mask.Size);

            var path = new PathF();
            path.AppendRoundedRectangle(bounds,
                GetRadius(mask.Corners.TopLeft),
                GetRadius(mask.Corners.TopRight),
                GetRadius(mask.Corners.BottomLeft),
                GetRadius(mask.Corners.BottomRight));

            LayoutBounds(mask, bounds, context, false);
            context.Canvas.ClipPath(path);

            float GetRadius(Dimensions cornerSize) => (float)cornerSize.Width.GetDrawPixels((int)bounds.Width, 1);
        }
    }
}
