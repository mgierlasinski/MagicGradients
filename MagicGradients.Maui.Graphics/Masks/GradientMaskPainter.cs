using MagicGradients.Masks;
using MagicGradients.Maui.Graphics.Drawing;
using Microsoft.Maui.Graphics;
using System;

namespace MagicGradients.Maui.Graphics.Masks
{
    public interface IEllipseMaskPainter<TContext>
    {
        void Clip(EllipseMask mask, TContext context);
    }

    public interface IRectangleMaskPainter<TContext>
    {
        void Clip(RectangleMask mask, TContext context);
    }

    public interface ITextMaskPainter<TContext>
    {
        void Clip(TextMask mask, TContext context);
    }

    public interface IPathMaskPainter<TContext>
    {
        void Clip(PathMask mask, TContext context);
    }

    public abstract class GradientMaskPainter
    {
        protected RectangleF GetSizeRect(DrawContext context, Dimensions size)
        {
            var width = (float)size.Width.GetDrawPixels((int)context.CanvasRect.Width, 1);
            var height = (float)size.Height.GetDrawPixels((int)context.CanvasRect.Height, 1);

            return new RectangleF(0, 0, width, height);
        }

        protected void LayoutBounds(GradientMask mask, RectangleF bounds, DrawContext context, bool keepAspectRatio)
        {
            BeginLayout(mask, bounds, context);

            if (mask.Stretch == Stretch.None)
            {
                var scaleX = (float)context.RenderRect.Width / context.CanvasRect.Width;
                var scaleY = (float)context.RenderRect.Height / context.CanvasRect.Height;

                if (keepAspectRatio)
                {
                    var scale = Math.Max(scaleX, scaleY);
                    context.Canvas.Scale(scale, scale);
                }
                else
                    context.Canvas.Scale(scaleX, scaleY);
            }
            else
            {
                var scaleX = context.RenderRect.Width / bounds.Width;
                var scaleY = context.RenderRect.Height / bounds.Height;

                if (mask.Stretch == Stretch.AspectFit)
                {
                    var scale = Math.Min(scaleX, scaleY);
                    context.Canvas.Scale(scale, scale);
                }

                if (mask.Stretch == Stretch.AspectFill)
                {
                    var scale = Math.Max(scaleX, scaleY);
                    context.Canvas.Scale(scale, scale);
                }

                if (mask.Stretch == Stretch.Fill)
                    context.Canvas.Scale(scaleX, scaleY);
            }

            EndLayout(mask, bounds, context);
        }

        protected virtual void BeginLayout(GradientMask mask, RectangleF bounds, DrawContext context)
        {
            context.Canvas.Translate((float)context.RenderRect.Width / 2, (float)context.RenderRect.Height / 2);
        }

        protected virtual void EndLayout(GradientMask mask, RectangleF bounds, DrawContext context)
        {
            context.Canvas.Translate(-bounds.Center.X, -bounds.Center.Y);
        }
    }
}
