using MagicGradients.Forms.SkiaViews.Drawing;
using MagicGradients.Masks;
using SkiaSharp;
using System;

namespace MagicGradients.Forms.SkiaViews.Masks
{
    public abstract class GradientMaskPainter
    {
        protected SKRectI GetBounds(Dimensions size, DrawContext context)
        {
            var width = (int)size.Width.GetDrawPixels(context.CanvasRect.Width, context.PixelScaling);
            var height = (int)size.Height.GetDrawPixels(context.CanvasRect.Height, context.PixelScaling);

            return new SKRectI(0, 0, width, height);
        }

        protected void LayoutBounds(IGradientMask mask, SKRect bounds, DrawContext context, bool keepAspectRatio)
        {
            BeginLayout(mask, bounds, context);

            if (mask.Stretch == Stretch.None)
            {
                var scaleX = (float)context.RenderRect.Width / context.CanvasRect.Width;
                var scaleY = (float)context.RenderRect.Height / context.CanvasRect.Height;

                if (keepAspectRatio)
                    context.Canvas.Scale(Math.Max(scaleX, scaleY));
                else
                    context.Canvas.Scale(scaleX, scaleY);
            }
            else
            {
                var scaleX = context.RenderRect.Width / bounds.Width;
                var scaleY = context.RenderRect.Height / bounds.Height;

                if (mask.Stretch == Stretch.AspectFit)
                    context.Canvas.Scale(Math.Min(scaleX, scaleY));

                if (mask.Stretch == Stretch.AspectFill)
                    context.Canvas.Scale(Math.Max(scaleX, scaleY));

                if (mask.Stretch == Stretch.Fill)
                    context.Canvas.Scale(scaleX, scaleY);
            }

            EndLayout(mask, bounds, context);
        }

        protected virtual void BeginLayout(IGradientMask mask, SKRect bounds, DrawContext context)
        {
            context.Canvas.Translate((float)context.RenderRect.Width / 2, (float)context.RenderRect.Height / 2);
        }

        protected virtual void EndLayout(IGradientMask mask, SKRect bounds, DrawContext context)
        {
            context.Canvas.Translate(-bounds.MidX, -bounds.MidY);
        }
    }
}
