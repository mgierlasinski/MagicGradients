using System;

namespace MagicGradients
{
    internal static class GradientMath
    {
        public static double ToRadians(double degrees) => (Math.PI / 180) * degrees;
        public static double RotateBy180(double degrees) => (180 + degrees) % 360;
    }
}
