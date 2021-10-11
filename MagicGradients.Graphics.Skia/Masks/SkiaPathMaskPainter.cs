using MagicGradients.Graphics.Drawing;
using MagicGradients.Graphics.Masks;
using MagicGradients.Graphics.Skia.Drawing;
using MagicGradients.Masks;
using Microsoft.Maui.Graphics.Skia;
using SkiaSharp;

namespace MagicGradients.Graphics.Skia.Masks
{
    public class SkiaPathMaskPainter : MaskPainter, IMaskPainter<PathMask, DrawContext>
    {
        public void Clip(PathMask mask, DrawContext context)
        {
            if (!mask.IsActive || string.IsNullOrEmpty(mask.Data))
                return;

            using var path = SKPath.ParseSvgPathData(mask.Data);
            ClipPathNative(path, mask, context);
        }

        protected internal void ClipPathNative(SKPath path, PathMask mask, DrawContext context)
        {
            var canvas = context.Canvas as SkiaCanvasEx;
            if (canvas == null)
                return;

            path.GetTightBounds(out var bounds);

            LayoutBounds(mask, bounds.AsRectangleF(), context, true);
            canvas.ClipPath(path, mask.ClipMode.ToSkOperation());
            RestoreTransform(context.Canvas);
        }
    }
}
