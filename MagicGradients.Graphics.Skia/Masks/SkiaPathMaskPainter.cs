using MagicGradients.Graphics.Skia.Drawing;
using MagicGradients.Masks;
using MagicGradients.Maui.Graphics.Drawing;
using MagicGradients.Maui.Graphics.Masks;
using Microsoft.Maui.Graphics.Skia;
using SkiaSharp;

namespace MagicGradients.Graphics.Skia.Masks
{
    public class SkiaPathMaskPainter : GradientMaskPainter, IMaskPainter<PathMask, DrawContext>
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
            var canvas = (context.Canvas as SkiaCanvasEx)?.Canvas;
            if (canvas == null)
                return;

            using var canvasLock = new CanvasLock(canvas);

            path.GetTightBounds(out var bounds);
            LayoutBounds(mask, bounds.AsRectangleF(), context, true);

            canvas.ClipPath(path, mask.ClipMode.ToSkOperation());
        }

        //protected void LayoutBounds(GradientMask mask, SKRect bounds, DrawContext context, SKCanvas canvas, bool keepAspectRatio)
        //{
        //    BeginLayout(mask, bounds, context);

        //    if (mask.Stretch == Stretch.None)
        //    {
        //        var scaleX = (float)context.RenderRect.Width / context.CanvasRect.Width;
        //        var scaleY = (float)context.RenderRect.Height / context.CanvasRect.Height;

        //        if (keepAspectRatio)
        //            canvas.Scale(Math.Max(scaleX, scaleY));
        //        else
        //            canvas.Scale(scaleX, scaleY);
        //    }
        //    else
        //    {
        //        var scaleX = context.RenderRect.Width / bounds.Width;
        //        var scaleY = context.RenderRect.Height / bounds.Height;

        //        if (mask.Stretch == Stretch.AspectFit)
        //            canvas.Scale(Math.Min(scaleX, scaleY));

        //        if (mask.Stretch == Stretch.AspectFill)
        //            canvas.Scale(Math.Max(scaleX, scaleY));

        //        if (mask.Stretch == Stretch.Fill)
        //            context.Canvas.Scale(scaleX, scaleY);
        //    }

        //    EndLayout(mask, bounds, context);
        //}

        //protected virtual void BeginLayout(GradientMask mask, SKRect bounds, DrawContext context)
        //{
        //    context.Canvas.Translate((float)context.RenderRect.Width / 2, (float)context.RenderRect.Height / 2);
        //}

        //protected virtual void EndLayout(GradientMask mask, SKRect bounds, DrawContext context)
        //{
        //    context.Canvas.Translate(-bounds.MidX, -bounds.MidY);
        //}
    }
}
