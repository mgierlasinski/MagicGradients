using SkiaSharp;

namespace MagicGradients.Renderers
{
    public class RenderContext
    {
        public SKCanvas Canvas { get; set; }
        public SKPaint Paint { get; set; }
        public SKRectI CanvasRect { get; set; }
        public SKRectI RenderRect { get; set; }
    }
}
