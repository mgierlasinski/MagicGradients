using MagicGradients.Drawing;
using Microsoft.Maui.Graphics;

namespace MagicGradients
{
    public static class PrimitivesExtensions
    {
        public static float GetDrawPixels(this Offset offset, float sizeInPixels, float pixelScaling)
        {
            return offset.Type == OffsetType.Proportional
                ? (float)(offset.Value * sizeInPixels)
                : (float)(offset.Value * pixelScaling);
        }

        public static RectangleF GetDrawRectangle(this Dimensions size, DrawContext context)
        {
            return new RectangleF(0, 0,
                size.Width.GetDrawPixels(context.CanvasRect.Width, context.PixelScaling),
                size.Height.GetDrawPixels(context.CanvasRect.Height, context.PixelScaling));
        }
    }
}
