using MagicGradients.Graphics.Drawing;
using MagicGradients.Masks;
using Microsoft.Maui.Graphics;

namespace MagicGradients.Graphics.Masks
{
    public class PathMaskPainter : GradientMaskPainter, IMaskPainter<PathMask, DrawContext>
    {
        public void Clip(PathMask mask, DrawContext context)
        {
            if (!mask.IsActive || string.IsNullOrEmpty(mask.Data))
                return;

            var path = PathBuilder.Build(mask.Data);
            //var bounds = path.Bounds;  // Requires native GraphicsService
            var bounds = path.GetBoundsByFlattening();

            LayoutBounds(mask, bounds, context, false);
            context.Canvas.ClipPath(path);
            RestoreTransform(context.Canvas);
        }
    }
}
