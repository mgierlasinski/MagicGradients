using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using static MagicGradients.BackgroundRepeat;

namespace MagicGradients.Renderers
{
    public class GradientRenderer
    {
        private readonly GradientView _control;

        public GradientRenderer(GradientView control)
        {
            _control = control;
        }

        public RenderContext CreateContext(SKPaintSurfaceEventArgs e)
        {
            var context = new RenderContext
            {
                Canvas = e.Surface.Canvas,
                Paint = new SKPaint(),
                CanvasRect = e.Info.Rect
            };

            var size = _control.GradientSize;

            if (size.Width.Value > 0 && size.Height.Value > 0)
            {
                var width = size.Width.Type == OffsetType.Proportional
                    ? (float)size.Width.Value * context.CanvasRect.Width
                    : (float)size.Width.Value;

                var height = size.Height.Type == OffsetType.Proportional
                    ? (float)size.Height.Value * context.CanvasRect.Height
                    : (float)size.Height.Value;

                context.RenderRect = new SKRect(0, 0, width, height);
            }
            else
            {
                context.RenderRect = context.CanvasRect;
            }

            return context;
        }

        public void Render(RenderContext context, IGradientShader shader)
        {
            context.Paint.Shader = shader.Create(context);

            if (context.RenderRect != context.CanvasRect)
            {
                RenderTiled(context);
            }
            else
            {
                context.Canvas.DrawRect(context.RenderRect, context.Paint);
            }
        }

        private void RenderTiled(RenderContext context)
        {
            var width = context.CanvasRect.Size.Width;
            var height = context.CanvasRect.Size.Height;

            var tileWidth = context.RenderRect.Width;
            var tileHeight = context.RenderRect.Height;

            var rows = _control.GradientRepeat == Repeat || _control.GradientRepeat == RepeatY ? 
                Math.Ceiling(height / tileHeight) : 1;

            var cols = _control.GradientRepeat == Repeat || _control.GradientRepeat == RepeatX ? 
                Math.Ceiling(width / tileWidth) : 1;

            for (var row = 0; row < rows; row++)
            {
                for (var col = 0; col < cols; col++)
                {
                    var point = new SKPoint(col * tileWidth, row * tileHeight);

                    context.Canvas.Save();
                    context.Canvas.Translate(point);
                    context.Canvas.DrawRect(context.RenderRect, context.Paint);
                    //DebugTile(context);
                    context.Canvas.Restore();
                }
            }
        }

        private void DebugTile(RenderContext context)
        {
            using (var paint = new SKPaint())
            {
                paint.StrokeWidth = 2;
                paint.Style = SKPaintStyle.Stroke;
                paint.Color = SKColors.Yellow;
                context.Canvas.DrawRect(context.RenderRect, paint);
            }
        }
    }
}
