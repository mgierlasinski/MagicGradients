using SkiaSharp;
using SkiaSharp.Views.Forms;

namespace Playground.Controls
{
    public class CheckeredView : SKCanvasView
    {
        private readonly SKColor _color1 = new SKColor(200, 200, 200, 255);
        private readonly SKColor _color2 = new SKColor(0, 0, 0, 0);

        public int GridSize { get; set; } = 40;

        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            base.OnPaintSurface(e);

            var info = e.Info;
            var canvas = e.Surface.Canvas;

            canvas.Clear(SKColors.White);

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
    }
}
