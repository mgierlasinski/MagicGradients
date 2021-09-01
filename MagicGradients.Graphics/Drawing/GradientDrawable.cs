using System;
using System.Linq;
using MagicGradients.Graphics.Masks;
using Microsoft.Maui.Graphics;
using Xamarin.Forms;
using static MagicGradients.BackgroundRepeat;
using PaintGradientStop = Microsoft.Maui.Graphics.GradientStop;
using Point = Microsoft.Maui.Graphics.Point;
using Size = Microsoft.Maui.Graphics.Size;

namespace MagicGradients.Graphics.Drawing
{
    public class GradientDrawable<T> : IDrawable where T : VisualElement, IGradientControl
    {
        private readonly T _control;
        public MaskDrawable<DrawContext> MaskDrawable { get; }

        public GradientDrawable(T control)
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
                gradient.Measure((int)context.RenderRect.Width, (int)context.RenderRect.Height);

                var paint = GetPaint(gradient, context);
                canvas.SetFillPaint(paint, context.RenderRect);

                DrawGradient(context);
            }
        }

        private Paint GetPaint(Gradient gradient, DrawContext context)
        {
            if (gradient is LinearGradient linear)
                return CreateLinearPaint(linear, context);

            if (gradient is RadialGradient radial)
                return CreateRadialPaint(radial, context);

            return new SolidPaint(Colors.Black);
        }

        private Paint CreateLinearPaint(LinearGradient gradient, DrawContext context)
        {
            var rect = context.RenderRect;

            var renderStops = GetRenderStops(gradient);
            var line = new LinearGradientGeometry(gradient, rect);
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

        private Paint CreateRadialPaint(RadialGradient gradient, DrawContext context)
        {
            var rect = context.RenderRect;

            var renderStops = GetRenderStops(gradient);
            var lastOffset = gradient.IsRepeating ? renderStops.LastOrDefault()?.Offset ?? 1 : 1;
            var circle = new RadialGradientGeometry(gradient, rect, lastOffset, context.PixelScaling);

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

        protected PaintGradientStop[] GetRenderStops(Gradient gradient)
        {
            if (gradient.Stops.Count == 1)
            {
                return new[]
                {
                    new PaintGradientStop(0, gradient.Stops[0].Color.ToMauiColor()),
                    new PaintGradientStop(1, gradient.Stops[0].Color.ToMauiColor())
                };
            }

            return gradient.Stops
                .OrderBy(x => x.RenderOffset)
                .Select(x => new PaintGradientStop(x.RenderOffset, x.Color.ToMauiColor()))
                .ToArray();
        }
    }
}
