using System;

namespace MagicGradients
{
    public static class GradientMath
    {
        public static double ToRadians(double degrees) => (Math.PI / 180) * degrees;
        public static double FromDegrees(double degrees) => (180 + degrees) % 360;
    }
}
