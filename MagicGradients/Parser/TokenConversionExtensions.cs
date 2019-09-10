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
    }
}
