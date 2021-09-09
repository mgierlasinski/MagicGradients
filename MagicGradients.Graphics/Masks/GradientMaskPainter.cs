using MagicGradients.Graphics.Drawing;
using MagicGradients.Masks;
using Microsoft.Maui.Graphics;
using System;
using System.Collections.Generic;

namespace MagicGradients.Graphics.Masks
{
    public abstract class GradientMaskPainter
    {
        private readonly Stack<AffineTransform> _transforms = new Stack<AffineTransform>();
        //private AffineTransform _transform = new AffineTransform();

        protected RectangleF GetBounds(Dimensions size, DrawContext context)
        {
            var width = (float)size.Width.GetDrawPixels((int)context.CanvasRect.Width, context.PixelScaling);
            var height = (float)size.Height.GetDrawPixels((int)context.CanvasRect.Height, context.PixelScaling);

            return new RectangleF(0, 0, width, height);
        }

        protected void LayoutBounds(GradientMask mask, RectangleF bounds, DrawContext context, bool keepAspectRatio)
        {
            BeginLayout(mask, bounds, context);

            if (mask.Stretch == Stretch.None)
            {
                var scaleX = context.RenderRect.Width / context.CanvasRect.Width;
                var scaleY = context.RenderRect.Height / context.CanvasRect.Height;

                if (keepAspectRatio)
                {
                    var scale = Math.Max(scaleX, scaleY);
                    Scale(context.Canvas, scale, scale);
                }
                else
                    Scale(context.Canvas, scaleX, scaleY);
            }
            else
            {
                var scaleX = context.RenderRect.Width / bounds.Width;
                var scaleY = context.RenderRect.Height / bounds.Height;

                if (mask.Stretch == Stretch.AspectFit)
                {
                    var scale = Math.Min(scaleX, scaleY);
                    Scale(context.Canvas, scale, scale);
                }

                if (mask.Stretch == Stretch.AspectFill)
                {
                    var scale = Math.Max(scaleX, scaleY);
                    Scale(context.Canvas, scale, scale);
                }

                if (mask.Stretch == Stretch.Fill)
                    Scale(context.Canvas, scaleX, scaleY);
            }

            EndLayout(mask, bounds, context);
        }

        protected virtual void BeginLayout(GradientMask mask, RectangleF bounds, DrawContext context)
        {
            Translate(context.Canvas, context.RenderRect.Width / 2, context.RenderRect.Height / 2);
        }

        protected virtual void EndLayout(GradientMask mask, RectangleF bounds, DrawContext context)
        {
            Translate(context.Canvas, -bounds.Center.X, -bounds.Center.Y);
        }
        
        protected void Translate(ICanvas canvas, float tx, float ty)
        {
            canvas.Translate(tx, ty);
            _transforms.Push(AffineTransform.GetTranslateInstance(tx, ty));
            //_transform.Translate(tx, ty);
        }

        protected void Scale(ICanvas canvas, float sx, float sy)
        {
            canvas.Scale(sx, sy);
            _transforms.Push(AffineTransform.GetScaleInstance(sx,sy));
            //_transform.Scale(sx, sy);
        }

        protected void RestoreTransform(ICanvas canvas)
        {
            while (_transforms.Count > 0)
            {
                var transform = _transforms.Pop();

                if (transform.ScaleX > 0 && transform.ScaleY > 0)
                    canvas.Scale(1 / transform.ScaleX, 1 / transform.ScaleY);

                if (transform.TranslateX != 0 || transform.TranslateY != 0)
                    canvas.Translate(-transform.TranslateX, -transform.TranslateY);
            }
        }

        //private void RestoreWithMatrix(ICanvas canvas)
        //{
        //    var movX = -_transform.TranslateX;
        //    var movY = -_transform.TranslateY;
        //    var scaleX = 1 / _transform.ScaleX;
        //    var scaleY = 1 / _transform.ScaleY;

        //    var inverseTransform = AffineTransform.GetScaleInstance(scaleX, scaleY);
        //    inverseTransform.Translate(movX, movY);

        //    canvas.ConcatenateTransform(inverseTransform);
        //    _transform = new AffineTransform();
        //}
    }
}
