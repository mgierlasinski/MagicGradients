using MagicGradients.Masks;
using Microsoft.Maui.Graphics;
using SkiaSharp;

namespace MagicGradients.Graphics.Skia
{
    public static class SkiaExtensions
    {
        public static SKColor ToSKColor(this Color color) =>
            new SKColor((byte)(color.Red * 255), (byte)(color.Green * 255), (byte)(color.Blue * 255), (byte)(color.Alpha * 255));

        public static SKColorF ToSKColorF(this Color color) =>
            new SKColorF(color.Red, color.Green, color.Blue, color.Alpha);

        public static SKPoint ToSKPoint(this PointF point) =>
            new SKPoint(point.X, point.Y);

        public static RectangleF ToRectF(this SKRectI rect) =>
            new RectangleF(rect.Left, rect.Top, rect.Width, rect.Height);

        public static SKClipOperation ToSkOperation(this ClipMode mode) =>
            mode == ClipMode.Include ? SKClipOperation.Intersect : SKClipOperation.Difference;
    }
}
