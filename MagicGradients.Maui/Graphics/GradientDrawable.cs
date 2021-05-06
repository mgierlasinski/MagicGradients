using Microsoft.Maui.Graphics;

namespace MagicGradients.Maui.Graphics
{
    public class GradientDrawable : IDrawable
    {
        public void Draw(ICanvas canvas, RectangleF dirtyRect)
        {
            var start = new Point(0, 0);
            var end = new Point(1, 1);

            var paint = new LinearGradientPaint(new[]
            {
                new GradientStop(0f, Colors.Orange),
                new GradientStop(0.5f, Colors.Blue),
                new GradientStop(1f, Colors.Yellow)
            }, start, end);

            canvas.SetFillPaint(paint, dirtyRect);
            canvas.FillRectangle(dirtyRect);
        }
    }
}
