using System;
using System.Globalization;

namespace MagicGradients
{
    public static class StringExtensions
    {
        public static bool TryExtractNumber(this string token, string unit, out double result)
        {
            if (token.EndsWith(unit))
            {
                var index = token.LastIndexOf(unit, StringComparison.OrdinalIgnoreCase);
                var number = token.Substring(0, index);

                if (double.TryParse(number, NumberStyles.Any, CultureInfo.InvariantCulture, out var value))
                {
                    result = value;
                    return true;
                }
            }

            result = 0;
            return false;
        }
    }
}
