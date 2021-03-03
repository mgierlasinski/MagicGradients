using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using Xamarin.Forms;
using static MagicGradients.BackgroundRepeat;

namespace MagicGradients.Renderers
{
    public class GradientRenderer
    {
        private readonly IGradientControl _control;

        public GradientRenderer(IGradientControl control)
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

            CalculateRenderSize(context);
            return context;
        }

        public RenderContext CreateContext(SKPaintGLSurfaceEventArgs e)
        {
            var context = new RenderContext
            {
                Canvas = e.Surface.Canvas,
                Paint = new SKPaint(),
                CanvasRect = e.BackendRenderTarget.Rect
            };

            CalculateRenderSize(context);
            return context;
        }

        private void CalculateRenderSize(RenderContext context)
        {
            var size = _control.GradientSize;

            if (size.Width.Value > 0 && size.Height.Value > 0)
            {
                var width = size.Width.Type == OffsetType.Proportional
                    ? size.Width.Value * context.CanvasRect.Width
                    : size.Width.Value;

                var height = size.Height.Type == OffsetType.Proportional
                    ? size.Height.Value * context.CanvasRect.Height
                    : size.Height.Value;

                context.RenderRect = new SKRectI(0, 0, (int)width, (int)height);
            }
            else
            {
                context.RenderRect = context.CanvasRect;
            }
        }

        public void PaintSurface(RenderContext context)
        {
            using (context.Paint)
            {
                foreach (var gradient in _control.GradientSource.GetGradients())
                {
                    if (gradient.Shader == null)
                        gradient.PrepareShader((BindableObject)_control);

                    gradient.Measure(context.RenderRect.Width, context.RenderRect.Height);
                    Render(context, gradient.Shader);
                }
            }
        }

        private void Render(RenderContext context, IGradientShader shader)
        {
            context.Paint.Shader = shader.Create(context);

            if (context.RenderRect != context.CanvasRect)
            {
                RenderTiled(context);
            }
            else
            {
                _control.Mask?.Clip(context);
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
                    _control.Mask?.Clip(context);
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
