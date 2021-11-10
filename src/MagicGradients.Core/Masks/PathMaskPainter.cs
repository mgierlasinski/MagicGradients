using MagicGradients.Drawing;
using Microsoft.Maui.Graphics;

namespace MagicGradients.Masks
{
    public class PathMaskPainter : IMaskPainter<IPathMask, DrawContext>
    {
        public void Clip(IPathMask mask, DrawContext context)
        {
            if (!mask.IsActive || string.IsNullOrEmpty(mask.Data))
                return;

            var path = PathBuilder.Build(mask.Data);
            //var bounds = path.Bounds;  // Requires native GraphicsService
            var bounds = path.GetBoundsByFlattening();

            using var layout = ShapeMaskLayout.Create(mask, bounds, context, false);
            context.Canvas.ClipPath(path);
        }
    }
}
