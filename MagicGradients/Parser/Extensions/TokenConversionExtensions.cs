using System;
using System.Globalization;
using Xamarin.Forms.Internals;

namespace MagicGradients.Parser.Extensions
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

        public static double ParseColorValue(this string elem, int maxValue, bool acceptPercent)
        {
            elem = elem.Trim();
            if (elem.EndsWith("%", StringComparison.Ordinal) && acceptPercent)
            {
                maxValue = 100;
                elem = elem.Substring(0, elem.Length - 1);
            }
            return (double)(int.Parse(elem, NumberStyles.Number, CultureInfo.InvariantCulture).Clamp(0, maxValue)) / maxValue;
        }
    }
}
