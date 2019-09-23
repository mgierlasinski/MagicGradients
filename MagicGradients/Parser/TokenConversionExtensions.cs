using System;
using System.Globalization;

namespace MagicGradients.Parser
{
    public static class TokenConversionExtensions
    {
        public static bool TryConvertPercentToOffset(this string token, out float result)
        {
            if (token.Contains("%"))
            {
                var percent = token.Replace("%", "");

                if (float.TryParse(percent, NumberStyles.Any, CultureInfo.InvariantCulture, out var value))
                {
                    result = Math.Min(value / 100, 1f); // Make sure no bigger than 1
                    return true;
                }
            }

            result = 0;
            return false;
        }

        public static bool TryConvertDirectionToAngle(this string token, out int result)
        {
            if (token.Contains("deg"))
            {
                var degree = token.Replace("deg", "");

                if (int.TryParse(degree, out var angle))
                {
                    result = (180 + angle) % 360;
                    return true;
                }
            }

            result = 0;
            return false;
        }

        public static double ToDouble(this string token, double @default = default)
        {
            if (double.TryParse(token, NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
            {
                return result;
            }

            return @default;
        }
    }
}
