using MagicGradients.Masks;
using MagicGradients.Maui.Graphics.Drawing;
using Microsoft.Maui.Graphics;

namespace MagicGradients.Maui.Graphics.Masks
{
    public class EllipseMaskPainter : GradientMaskPainter, IMaskPainter<EllipseMask, DrawContext>
    {
        public void Clip(EllipseMask mask, DrawContext context)
        {
            if (!mask.IsActive)
                return;

            var bounds = GetBounds(mask.Size, context);

            var path = new PathF();
            path.AppendEllipse(bounds);

            LayoutBounds(mask, bounds, context, false);
            context.Canvas.ClipPath(path);
        }
    }
}
