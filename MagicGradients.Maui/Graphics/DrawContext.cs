using Microsoft.Maui.Graphics;

namespace MagicGradients.Maui.Graphics
{
    public class DrawContext
    {
        public ICanvas Canvas { get; }
        public RectangleF CanvasRect { get; }
        public RectangleF RenderRect { get; private set; }
        //public double PixelScaling { get; private set; }

        public DrawContext(ICanvas canvas, RectangleF canvasRect)
        {
            Canvas = canvas;
            CanvasRect = canvasRect;
        }

        public void Measure(Dimensions size/*, double viewWidth*/)
        {
            if (size.Width.Value > 0 && size.Height.Value > 0)
            {
                var width = size.Width.Type == OffsetType.Proportional
                    ? size.Width.Value * CanvasRect.Width
                    : size.Width.Value;

                var height = size.Height.Type == OffsetType.Proportional
                    ? size.Height.Value * CanvasRect.Height
                    : size.Height.Value;

                RenderRect = new RectangleF(0, 0, (int)width, (int)height);
            }
            else
            {
                RenderRect = CanvasRect;
            }

            //PixelScaling = CanvasRect.Width / viewWidth;
        }
    }
}
