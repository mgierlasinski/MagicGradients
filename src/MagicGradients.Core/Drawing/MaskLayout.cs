using MagicGradients.Masks;
using Microsoft.Maui.Graphics;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace MagicGradients.Drawing
{
    public class MaskLayout
    {
        private readonly Stack<Matrix3x2> _transforms = new();
        
        public void LayoutBounds(IGradientMask mask, RectF bounds, DrawContext context, bool keepAspectRatio)
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

        protected virtual void BeginLayout(IGradientMask mask, RectF bounds, DrawContext context)
        {
            Translate(context.Canvas, context.RenderRect.Width / 2, context.RenderRect.Height / 2);
        }

        protected virtual void EndLayout(IGradientMask mask, RectF bounds, DrawContext context)
        {
            Translate(context.Canvas, -bounds.Center.X, -bounds.Center.Y);
        }

        protected void Translate(ICanvas canvas, float tx, float ty)
        {
            canvas.Translate(tx, ty);
            _transforms.Push(Matrix3x2.CreateTranslation(tx, ty));
        }

        protected void Scale(ICanvas canvas, float sx, float sy)
        {
            canvas.Scale(sx, sy);
            _transforms.Push(Matrix3x2.CreateScale(sx, sy));
        }

        public void RestoreTransform(ICanvas canvas)
        {
            while (_transforms.Count > 0)
            {
                var transform = _transforms.Pop();
                var scaleX = transform.M11;
                var scaleY = transform.M22;
                var transX = transform.M31;
                var transY = transform.M32;

                if (scaleX > 0 && scaleY > 0)
                    canvas.Scale(1 / scaleX, 1 / scaleY);

                if (transX != 0 || transY != 0)
                    canvas.Translate(-transX, -transY);
            }
        }
    }

    public class ShapeMaskLayout : MaskLayout, IDisposable
    {
        private ICanvas _canvas;

        public static ShapeMaskLayout Create(IGradientMask mask, RectF bounds, DrawContext context, bool keepAspectRatio)
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

        public static TextMaskLayout Create(ITextMask mask, RectF bounds, DrawContext context)
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

        protected override void BeginLayout(IGradientMask mask, RectF bounds, DrawContext context)
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

        protected override void EndLayout(IGradientMask mask, RectF bounds, DrawContext context)
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
