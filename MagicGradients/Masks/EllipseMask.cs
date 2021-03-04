﻿using MagicGradients.Renderers;
using SkiaSharp;

namespace MagicGradients.Masks
{
    public class EllipseMask : RectangleMask
    {
        public override void Clip(RenderContext context)
        {
            if(!IsActive)
                return;

            var width = (int)Size.Width.GetPixels(context.CanvasRect.Width);
            var height = (int)Size.Height.GetPixels(context.CanvasRect.Height);

            var bounds = new SKRectI(0, 0, width, height);
            var ellipse = new SKRoundRect(bounds, (float)width / 2, (float)height / 2);

            ClipRoundRect(context, ellipse);
        }
    }
}
