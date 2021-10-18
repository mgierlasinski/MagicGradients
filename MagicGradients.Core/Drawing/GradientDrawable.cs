using MagicGradients.Masks;
using Microsoft.Maui.Graphics;
using System;
using System.Linq;
using static MagicGradients.BackgroundRepeat;
using PaintGradientStop = Microsoft.Maui.Graphics.GradientStop;

namespace MagicGradients.Drawing
{
    public class GradientDrawable : IDrawable
    {
        private readonly IGradientControl _control;
        public MaskDrawable<DrawContext> MaskDrawable { get; }

        public GradientDrawable(IGradientControl control)
        {
            _control = control;

            MaskDrawable = new MaskDrawable<DrawContext>(
                new EllipseMaskPainter(), 
                new RectangleMaskPainter(), 
                new TextMaskPainter(), 
                new PathMaskPainter());
        }

        public void Draw(ICanvas canvas, RectangleF dirtyRect)
        {
            if (_control.GradientSource == null)
                return;

            var context = new DrawContext(canvas, dirtyRect);
            context.Measure(_control.GradientSize, _control.Width);

            foreach (var gradient in _control.GradientSource.GetGradients())
            {
                var paint = GetPaint(gradient, context);
                canvas.SetFillPaint(paint, context.RenderRect);

                DrawGradient(context);
            }
        }

        private Paint GetPaint(IGradient gradient, DrawContext context)
        {
            if (gradient is ILinearGradient linear)
                return CreateLinearPaint(linear, context);

            if (gradient is IRadialGradient radial)
                return CreateRadialPaint(radial, context);

            return new SolidPaint(Colors.Black);
        }

        private Paint CreateLinearPaint(ILinearGradient gradient, DrawContext context)
        {
            var rect = context.RenderRect;

            var line = new LinearGradientGeometry();
            line.CalculateOffsets(gradient, (int)context.RenderRect.Width, (int)context.RenderRect.Height);
            line.CalculateGeometry(gradient, rect);

            var renderStops = GetRenderStops(gradient);
            var start = line.Start;
            var end = line.End;

            if (gradient.IsRepeating)
            {
                var firstOffset = renderStops.FirstOrDefault()?.Offset ?? 0;
                var lastOffset = renderStops.LastOrDefault()?.Offset ?? 1;

                start = line.GetColorPointAt(firstOffset);
                end = line.GetColorPointAt(lastOffset);

                foreach (var stop in renderStops)
                {
                    stop.Offset = line.ScaleWithBias(stop.Offset, firstOffset, lastOffset, 0, 1);
                }
            }

            // Convert pixels to proportional
            var startPoint = new Point(start.X / rect.Width, start.Y / rect.Height);
            var endPoint = new Point(end.X / rect.Width, end.Y / rect.Height);

            return new LinearGradientPaintEx(renderStops, startPoint, endPoint, gradient.IsRepeating);
        }

        private Paint CreateRadialPaint(IRadialGradient gradient, DrawContext context)
        {
            var rect = context.RenderRect;

            var circle = new RadialGradientGeometry();
            circle.CalculateOffsets(gradient, (int)context.RenderRect.Width, (int)context.RenderRect.Height);

            var renderStops = GetRenderStops(gradient);
            var lastOffset = gradient.IsRepeating ? renderStops.LastOrDefault()?.Offset ?? 1 : 1;

            circle.CalculateGeometry(gradient, rect, lastOffset, context.PixelScaling);

            foreach (var stop in renderStops)
            {
                stop.Offset = lastOffset > 0 ? stop.Offset / lastOffset : 0;
            }

            // Convert pixels to proportional
            var center = new Point(circle.Center.X / rect.Width, circle.Center.Y / rect.Height);
            var radius = new Size(circle.Radius.Width / rect.Width, circle.Radius.Height / rect.Height);

            return new RadialGradientPaintEx(renderStops, center, radius, gradient.IsRepeating);
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
                    MaskDrawable.Clip(_control.Mask, context);
                    context.Canvas.FillRectangle(context.RenderRect);
                    context.Canvas.RestoreState();
                }
            }
        }

        protected PaintGradientStop[] GetRenderStops(IGradient gradient)
        {
            var stops = gradient.GetStops();
            if (stops.Count == 1)
            {
                return new[]
                {
                    new PaintGradientStop(0, stops[0].Color),
                    new PaintGradientStop(1, stops[0].Color)
                };
            }

            return stops
                .OrderBy(x => x.RenderOffset)
                .Select(x => new PaintGradientStop(x.RenderOffset, x.Color))
                .ToArray();
        }
    }
}
