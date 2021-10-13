using MagicGradients.Drawing;
using SkiaSharp;
using System;
using Xamarin.Forms;
using static MagicGradients.BackgroundRepeat;

namespace MagicGradients.Skia.Forms.Drawing
{
    public class GradientDrawable<T> where T : VisualElement, IGradientControl
    {
        private readonly T _control;
        private readonly LinearGradientPainter _linearPainter;
        private readonly RadialGradientPainter _radialPainter;
        private readonly MaskDrawable<DrawContext> _maskDrawable;

        public GradientDrawable(T control)
        {
            _control = control;
            _linearPainter = new LinearGradientPainter();
            _radialPainter = new RadialGradientPainter();
            _maskDrawable = new MaskDrawable<DrawContext>(
                new Masks.EllipseMaskPainter(),
                new Masks.RectangleMaskPainter(),
                new Masks.TextMaskPainter(),
                new Masks.PathMaskPainter());
        }
        
        public void Draw(DrawContext context)
        {
            context.Canvas.Clear();

            if(_control.GradientSource == null)
                return;
            
            using (context.Paint)
            {
                foreach (var gradient in _control.GradientSource.GetGradients())
                {
                    //gradient.Measure(context.RenderRect.Width, context.RenderRect.Height);
                    context.Paint.Shader = GetShader(gradient, context);
                    DrawGradient(context);
                }
            }
        }

        private SKShader GetShader(Gradient gradient, DrawContext context)
        {
            if (gradient is LinearGradient linear)
                return _linearPainter.CreateShader(linear, context);

            if (gradient is RadialGradient radial)
                return _radialPainter.CreateShader(radial, context);

            throw new ArgumentException("Type not supported");
        }

        private void DrawGradient(DrawContext context)
        {
            var width = context.CanvasRect.Size.Width;
            var height = context.CanvasRect.Size.Height;

            var tileWidth = context.RenderRect.Width;
            var tileHeight = context.RenderRect.Height;

            var rows = _control.GradientRepeat == Repeat || _control.GradientRepeat == RepeatY ?
                (int)Math.Ceiling((double)height / tileHeight) : 1;

            var cols = _control.GradientRepeat == Repeat || _control.GradientRepeat == RepeatX ?
                (int)Math.Ceiling((double)width / tileWidth) : 1;

            for (var row = 0; row < rows; row++)
            {
                for (var col = 0; col < cols; col++)
                {
                    var point = new SKPoint(col * tileWidth, row * tileHeight);

                    context.Canvas.Save();
                    context.Canvas.Translate(point);
                    _maskDrawable.Clip(_control.Mask, context);
                    context.Canvas.DrawRect(context.RenderRect, context.Paint);
                    context.Canvas.Restore();
                }
            }
        }
    }
}
