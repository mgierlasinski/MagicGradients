using Microsoft.Maui.Graphics;
using System;

namespace MagicGradients.Drawing
{
    public class DrawContext
    {
        public ICanvas Canvas { get; }
        public RectF CanvasRect { get; }
        public RectF RenderRect { get; private set; }
        public float PixelScaling { get; private set; }

        public DrawContext(ICanvas canvas, RectF canvasRect)
        {
            Canvas = canvas;
            CanvasRect = canvasRect;
        }

        public void Measure(Dimensions size, double viewWidth)
        {
            PixelScaling = viewWidth > 0 ? (float)(CanvasRect.Width / viewWidth) : 1;

            if (size.Width.Value > 0 && size.Height.Value > 0)
            {
                var width = size.Width.Type == OffsetType.Proportional
                    ? size.Width.Value * CanvasRect.Width
                    : size.Width.Value * PixelScaling;

                var height = size.Height.Type == OffsetType.Proportional
                    ? size.Height.Value * CanvasRect.Height
                    : size.Height.Value * PixelScaling;

                RenderRect = new RectF(0, 0, (int)width, (int)height);
            }
            else
            {
                RenderRect = CanvasRect;
            }
        }

        public TCanvas GetNativeCanvas<TCanvas>() where TCanvas : ICanvas
        {
            if (Canvas is TCanvas canvas)
                return canvas;

            throw new InvalidCastException($"Cannot cast native canvas {Canvas.GetType().Name} into {typeof(TCanvas).Name}");
        }
    }
}
