using SkiaSharp;

namespace MagicGradients.Renderers
{
    public class RenderContext
    {
        public SKCanvas Canvas { get; set; }
        public SKPaint Paint { get; set; }
        public SKRect CanvasRect { get; set; }
        public SKRect RenderRect { get; set; }
    }
}
