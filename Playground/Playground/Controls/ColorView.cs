using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace Playground.Controls
{
    public class ColorView : SKCanvasView
    {
        private readonly SKColor _color1 = new SKColor(200, 200, 200, 255);
        private readonly SKColor _color2 = new SKColor(0, 0, 0, 0);

        public static readonly BindableProperty ColorProperty = BindableProperty.Create(
            nameof(Color), typeof(Color), typeof(ColorView),
                propertyChanged: (b, value, newValue) => ((ColorView)b).InvalidateSurface());

        public Color Color
        {
            get => (Color)GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }

        public int GridSize { get; set; } = 40;

        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            base.OnPaintSurface(e);

            var canvas = e.Surface.Canvas;
            canvas.Clear(SKColors.White);

            DrawBackground(canvas, e.Info);

            if (Color != Color.Default)
            {
                DrawColor(canvas, e.Info);
            }
        }

        private void DrawBackground(SKCanvas canvas, SKImageInfo info)
        {
            using (var paint = new SKPaint())
            {
                var topBottom = SKShader.CreateLinearGradient(
                    new SKPoint(0, 0),
                    new SKPoint(0, GridSize),
                    new[] { _color1, _color1, _color2, _color2 },
                    new[] { 0f, 0.5f, 0.5f, 1f },
                    SKShaderTileMode.Repeat);

                var leftRight = SKShader.CreateLinearGradient(
                    new SKPoint(0, 0),
                    new SKPoint(GridSize, 0),
                    new[] { _color1, _color1, _color2, _color2 },
                    new[] { 0f, 0.5f, 0.5f, 1f },
                    SKShaderTileMode.Repeat);

                var shader = SKShader.CreateCompose(topBottom, leftRight, SKBlendMode.Xor);

                paint.Shader = shader;
                canvas.DrawRect(0, 0, info.Width, info.Height, paint);
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
