﻿using MagicGradients.Masks;
using MagicGradients.Maui.Graphics.Masks;
using MagicGradients.Skia.Forms.Drawing;
using SkiaSharp;

namespace MagicGradients.Skia.Forms.Masks
{
    public class PathMaskPainter : GradientMaskPainter, IMaskPainter<PathMask, DrawContext>
    {
        public void Clip(PathMask mask, DrawContext context)
        {
            if (!mask.IsActive || string.IsNullOrEmpty(mask.Data))
                return;

            using var path = SKPath.ParseSvgPathData(mask.Data);
            ClipPath(path, mask, context);
        }

        protected internal void ClipPath(SKPath path, PathMask mask, DrawContext context)
        {
            using var canvasLock = new CanvasLock(context.Canvas);

            path.GetTightBounds(out var bounds);
            LayoutBounds(mask, bounds, context, true);

            context.Canvas.ClipPath(path, mask.ClipMode.ToSkOperation());
        }
    }
}