using System;

namespace MagicGradients.Parser
{
    internal struct Vector2
    {
        public static Vector2 Zero { get; } = new Vector2(0d, 0d);
        public static Vector2 Left { get; } = new Vector2(-1d, 0d);
        public static Vector2 Right { get; } = new Vector2(1d, 0d);
        public static Vector2 Up { get; } = new Vector2(0d, -1d);
        public static Vector2 Down { get; } = new Vector2(0d, 1d);

        public double X { get; private set; }
        public double Y { get; private set; }

        private Vector2(double x, double y)
        {
            X = x;
            Y = y;
        }

        public void SetNamedDirection(string direction)
        {
            switch (direction)
            {
                case "left":
                    X = -1;
                    break;
                case "right":
                    X = 1;
                    break;
                case "top":
                    Y = -1;
                    break;
                case "bottom":
                    Y = 1;
                    break;
                case "center":
                    // center should be default:
                    // X = 0;
                    // Y = 0;
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"Unrecognized direction: '{direction}'");
            }
        }

        // http://james-ramsden.com/angle-between-two-vectors/
        public static double Angle(ref Vector2 value1, ref Vector2 value2)
        {
            var topPart = (value1.X * value2.X) + (value1.Y * value2.Y);

            var value1Squared = Math.Pow(value1.X, 2) + Math.Pow(value1.Y, 2);
            var value2Squared = Math.Pow(value2.X, 2) + Math.Pow(value2.Y, 2);

            var bottomPart = Math.Sqrt(value1Squared * value2Squared);

            var result = Math.Acos(topPart / bottomPart);   // Radians
            result *= 360.0 / (2 * Math.PI);                // Degrees

            return result;
        }
    }
}
