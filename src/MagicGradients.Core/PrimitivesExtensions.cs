﻿using Microsoft.Maui.Graphics;

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

        public static PointF GetDrawPoint(this Position position, RectangleF rect, float pixelScaling)
        {
            return new PointF(
                position.X.GetDrawPixels(rect.Width, pixelScaling),
                position.Y.GetDrawPixels(rect.Height, pixelScaling));
        }

        public static RectangleF GetDrawRectangle(this Dimensions dimensions, RectangleF rect, float pixelScaling)
        {
            return new RectangleF(0, 0,
                dimensions.Width.GetDrawPixels(rect.Width, pixelScaling),
                dimensions.Height.GetDrawPixels(rect.Height, pixelScaling));
        }
    }
}
