using MagicGradients.Drawing;
using MagicGradients.Forms.Skia.Drawing;
using MagicGradients.Masks;
using Microsoft.Maui.Graphics.Skia;
using SkiaSharp;

namespace MagicGradients.Forms.Skia.Masks
{
    public class SkiaPathMaskPainter : IMaskPainter<IPathMask, DrawContext>
    {
        public void Clip(IPathMask mask, DrawContext context)
        {
            if (!mask.IsActive || string.IsNullOrEmpty(mask.Data))
                return;

            using var path = SKPath.ParseSvgPathData(mask.Data);
            path.GetTightBounds(out var bounds);

            var canvas = context.GetNativeCanvas<SkiaCanvasEx>();

            using var layout = ShapeMaskLayout.Create(mask, bounds.AsRectangleF(), context, true);
            canvas.ClipPath(path, mask.ClipMode.ToSkOperation());
        }
    }
}
