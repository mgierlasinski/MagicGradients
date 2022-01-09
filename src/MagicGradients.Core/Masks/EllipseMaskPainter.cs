using MagicGradients.Drawing;
using Microsoft.Maui.Graphics;

namespace MagicGradients.Masks
{
    public class EllipseMaskPainter : IMaskPainter<IEllipseMask, DrawContext>
    {
        public void Clip(IEllipseMask mask, DrawContext context)
        {
            if (!mask.IsActive)
                return;

            var bounds = mask.Size.GetDrawRectangle(context.CanvasRect, context.PixelScaling);

            var path = new PathF();
            path.AppendEllipse(bounds);

            using var layout = ShapeMaskLayout.Create(mask, bounds, context, false);
            context.Canvas.ClipPath(path);
        }
    }
}
