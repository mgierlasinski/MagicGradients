using SkiaSharp;

namespace MagicGradients.Masks
{
    public enum PathFill
    {
        Center,
        AspectFit,
        AspectFill,
        Fill
    }

    public enum ClipMode
    {
        Difference = 0,
        Intersect = 1
    }

    public static class ClipModeExtensions
    {
        public static SKClipOperation ToSkOperation(this ClipMode mode)
        {
            return (SKClipOperation)mode;
        }
    }
}
