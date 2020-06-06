using System;

namespace MagicGradients
{
    [Flags]
    public enum RadialGradientFlags
    {
        None = 0,
        XProportional = 1 << 0,
        YProportional = 1 << 1,
        WidthProportional = 1 << 2,
        HeightProportional = 1 << 3,
        PositionProportional = 1 | 1 << 1,
        SizeProportional = 1 << 2 | 1 << 3,
        All = ~0
    }

    public enum RadialGradientShape
    {
        Ellipse,
        Circle
    }

    public enum RadialGradientSize
    {
        ClosestSide = 1,
        ClosestCorner = 2,
        FarthestSide = 3,
        FarthestCorner = 4
    }

    public static class RadialGradientSizeExtensions
    {
        public static bool IsClosest(this RadialGradientSize size) => (int)size < 3;
        public static bool IsFarthest(this RadialGradientSize size) => (int)size >= 3;
        public static bool IsCorner(this RadialGradientSize size) => (int)size % 2 == 0;
        public static bool IsSide(this RadialGradientSize size) => (int)size % 2 != 0;
    }
}
