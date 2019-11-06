using System;
using Xamarin.Forms;

namespace Playground.Extensions
{
    public static class ColorExtensions
    {
        public static bool IsCloseTo(this Color self, Color[] others, double tolerance)
        {
            foreach (var other in others)
            {
                var isCloseTo = Math.Abs(self.R - other.R) < tolerance &&
                                Math.Abs(self.G - other.G) < tolerance &&
                                Math.Abs(self.B - other.B) < tolerance;

                if (isCloseTo)
                    return true;
            }

            return false;
        }
    }
}
