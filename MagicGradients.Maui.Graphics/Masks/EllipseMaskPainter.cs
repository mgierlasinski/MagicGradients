using MagicGradients.Masks;
using MagicGradients.Maui.Graphics.Drawing;
using Microsoft.Maui.Graphics;

namespace MagicGradients.Maui.Graphics.Masks
{
    public class EllipseMaskPainter : GradientMaskPainter, IEllipseMaskPainter<DrawContext>
    {
        public void Clip(EllipseMask mask, DrawContext context)
        {
            if (!mask.IsActive)
                return;

            var bounds = GetSizeRect(context, mask.Size);

            var path = new PathF();
            path.AppendEllipse(bounds);

            LayoutBounds(mask, bounds, context, false);
            context.Canvas.ClipPath(path);
        }
    }
}
