using SkiaSharp;
using SkiaSharp.Views.Forms;

namespace MagicGradients.Skia.Forms.Drawing
{
    public class DrawContext
    {
        public SKCanvas Canvas { get; set; }
        public SKPaint Paint { get; set; }
        public SKRectI CanvasRect { get; set; }
        public SKRectI RenderRect { get; set; }
        public float PixelScaling { get; set; }

        public DrawContext(SKPaintSurfaceEventArgs e)
        {
            Canvas = e.Surface.Canvas;
            Paint = new SKPaint();
            CanvasRect = e.Info.Rect;
        }

        public DrawContext(SKPaintGLSurfaceEventArgs e)
        {
            Canvas = e.Surface.Canvas;
            Paint = new SKPaint();
            CanvasRect = e.BackendRenderTarget.Rect;
        }

        public void Measure(Dimensions size, double viewWidth)
        {
            PixelScaling = (float)(CanvasRect.Width / viewWidth);

            if (size.Width.Value > 0 && size.Height.Value > 0)
            {
                var width = size.Width.Type == OffsetType.Proportional
                    ? size.Width.Value * CanvasRect.Width
                    : size.Width.Value * PixelScaling;

                var height = size.Height.Type == OffsetType.Proportional
                    ? size.Height.Value * CanvasRect.Height
                    : size.Height.Value * PixelScaling;

                RenderRect = new SKRectI(0, 0, (int)width, (int)height);
            }
            else
            {
                RenderRect = CanvasRect;
            }
        }
    }
}
