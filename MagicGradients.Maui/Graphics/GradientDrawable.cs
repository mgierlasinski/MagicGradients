using Microsoft.Maui.Graphics;
using System;
using static MagicGradients.BackgroundRepeat;

namespace MagicGradients.Maui.Graphics
{
    public class GradientDrawable : IDrawable
    {
        private readonly IGradientControl _control;
        private readonly LinearGradientPainter _linearPainter;
        private readonly RadialGradientPainter _radialPainter;

        public GradientDrawable(IGradientControl control)
        {
            _control = control;
            _linearPainter = new LinearGradientPainter();
            _radialPainter = new RadialGradientPainter();
        }

        public void Draw(ICanvas canvas, RectangleF dirtyRect)
        {
            if (_control.GradientSource == null)
                return;

            var context = new DrawContext(canvas, dirtyRect);
            context.Measure(_control.GradientSize);

            foreach (var gradient in _control.GradientSource.GetGradients())
            {
                gradient.Measure((int)context.RenderRect.Width, (int)context.RenderRect.Height);

                var paint = GetPaint(gradient, context);
                canvas.SetFillPaint(paint, context.RenderRect);

                DrawGradient(context);
            }
        }

        private Paint GetPaint(Gradient gradient, DrawContext context)
        {
            if (gradient is LinearGradient linear)
                return _linearPainter.CreatePaint(linear, context);

            if (gradient is RadialGradient radial)
                return _radialPainter.CreatePaint(radial, context);

            return new SolidPaint(Colors.Black);
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
                    context.Canvas.SaveState();
                    context.Canvas.Translate(col * tileWidth, row * tileHeight);
                    context.Canvas.FillRectangle(context.RenderRect);
                    context.Canvas.RestoreState();
                }
            }
        }
    }
}
