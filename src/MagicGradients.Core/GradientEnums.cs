using MagicGradients.Converters;
using System.ComponentModel;

namespace MagicGradients
{
    [TypeConverter(typeof(BackgroundRepeatTypeConverter))]
    public enum BackgroundRepeat
    {
        Repeat, 
        RepeatX, 
        RepeatY, 
        NoRepeat
    }
    
    public enum RadialGradientShape
    {
        Ellipse,
        Circle
    }

    public enum RadialGradientStretch
    {
        ClosestSide = 1,
        ClosestCorner = 2,
        FarthestSide = 3,
        FarthestCorner = 4
    }

    public static class RadialGradientStretchExtensions
    {
        public static bool IsClosest(this RadialGradientStretch stretch) => (int)stretch < 3;
        public static bool IsFarthest(this RadialGradientStretch stretch) => (int)stretch >= 3;
        public static bool IsCorner(this RadialGradientStretch stretch) => (int)stretch % 2 == 0;
        public static bool IsSide(this RadialGradientStretch stretch) => (int)stretch % 2 != 0;
    }
}
