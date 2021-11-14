using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace Playground.Controls
{
    public class ColorView : CheckeredView
    {
        public static readonly BindableProperty ColorProperty = BindableProperty.Create(
            nameof(Color), typeof(Color), typeof(ColorView),
                propertyChanged: (b, value, newValue) => ((ColorView)b).InvalidateSurface());

        public Color Color
        {
            get => (Color)GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }

        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            base.OnPaintSurface(e);

            var canvas = e.Surface.Canvas;
            
            if (Color != Color.Default)
            {
                DrawColor(canvas, e.Info);
            }
        }

        private void DrawColor(SKCanvas canvas, SKImageInfo info)
        {
            using (var colorPaint = new SKPaint())
            {
                colorPaint.Color = Color.ToSKColor();
                colorPaint.Style = SKPaintStyle.Fill;
                canvas.DrawRect(0, 0, info.Width, info.Height, colorPaint);
            }
        }
    }
}
