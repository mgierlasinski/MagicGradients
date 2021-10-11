using MagicGradients.Graphics.Drawing;
using MagicGradients.Masks;
using Microsoft.Maui.Graphics;

namespace MagicGradients.Graphics.Masks
{
    public class EllipseMaskPainter : MaskPainter, IMaskPainter<EllipseMask, DrawContext>
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
            RestoreTransform(context.Canvas);
        }
    }
}
