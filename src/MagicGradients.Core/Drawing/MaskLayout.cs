using MagicGradients.Masks;
using Microsoft.Maui.Graphics;
using System;
using System.Collections.Generic;

namespace MagicGradients.Drawing
{
    public class MaskLayout
    {
        private readonly Stack<AffineTransform> _transforms = new();
        //private AffineTransform _transform = new AffineTransform();
        
        public void LayoutBounds(IGradientMask mask, RectangleF bounds, DrawContext context, bool keepAspectRatio)
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

        protected virtual void BeginLayout(IGradientMask mask, RectangleF bounds, DrawContext context)
        {
            Translate(context.Canvas, context.RenderRect.Width / 2, context.RenderRect.Height / 2);
        }

        protected virtual void EndLayout(IGradientMask mask, RectangleF bounds, DrawContext context)
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
            _transforms.Push(AffineTransform.GetScaleInstance(sx, sy));
            //_transform.Scale(sx, sy);
        }

        public void RestoreTransform(ICanvas canvas)
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

    public class ShapeMaskLayout : MaskLayout, IDisposable
    {
        private ICanvas _canvas;

        public static ShapeMaskLayout Create(IGradientMask mask, RectangleF bounds, DrawContext context, bool keepAspectRatio)
        {
            var layout = new ShapeMaskLayout { _canvas = context.Canvas };
            layout.LayoutBounds(mask, bounds, context, keepAspectRatio);

            return layout;
        }

        public void Dispose()
        {
            RestoreTransform(_canvas);
            _canvas = null;
        }
    }

    public class TextMaskLayout : MaskLayout, IDisposable
    {
        private ICanvas _canvas;

        public static TextMaskLayout Create(ITextMask mask, RectangleF bounds, DrawContext context)
        {
            var layout = new TextMaskLayout { _canvas = context.Canvas };
            layout.LayoutBounds(mask, bounds, context, true);

            return layout;
        }

        public void Dispose()
        {
            RestoreTransform(_canvas);
            _canvas = null;
        }

        protected override void BeginLayout(IGradientMask mask, RectangleF bounds, DrawContext context)
        {
            var textMask = (ITextMask)mask;

            var posX = textMask.HorizontalTextAlignment switch
            {
                TextAlignment.Center => context.RenderRect.Width / 2,
                TextAlignment.End => context.RenderRect.Width,
                _ => 0
            };

            var posY = textMask.VerticalTextAlignment switch
            {
                TextAlignment.Center => context.RenderRect.Height / 2,
                TextAlignment.End => context.RenderRect.Height,
                _ => 0
            };

            Translate(context.Canvas, posX, posY);
        }

        protected override void EndLayout(IGradientMask mask, RectangleF bounds, DrawContext context)
        {
            var textMask = (ITextMask)mask;

            var movX = textMask.HorizontalTextAlignment switch
            {
                TextAlignment.Center => -bounds.Center.X,
                TextAlignment.End => -bounds.Right,
                _ => -bounds.Left
            };

            var movY = textMask.VerticalTextAlignment switch
            {
                TextAlignment.Center => -bounds.Center.Y,
                TextAlignment.End => -bounds.Bottom,
                _ => -bounds.Top
            };

            Translate(context.Canvas, movX, movY);
        }
    }
}
