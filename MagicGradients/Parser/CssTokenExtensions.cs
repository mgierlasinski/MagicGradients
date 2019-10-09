using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace MagicGradients.Parser
{
    public static class CssTokenExtensions
    {
        public static bool TryExtractNumber(this string token, string unit, out float result)
        {
            if (token.EndsWith(unit))
            {
                var index = token.LastIndexOf(unit, StringComparison.OrdinalIgnoreCase);
                var number = token.Substring(0, index);

                if (float.TryParse(number, NumberStyles.Any, CultureInfo.InvariantCulture, out var value))
                {
                    result = value;
                    return true;
                }
            }

            result = 0;
            return false;
        }

        public static bool TryConvertOffset(this string token, out float result)
        {
            if (token != null)
            {
                if (token.TryExtractNumber("%", out var value))
                {
                    result = Math.Min(value / 100, 1f); // No bigger than 1
                    return true;
                }

                if (token.TryExtractNumber("px", out result))
                {
                    return true;
                }
            }

            result = 0;
            return false;
        }

        public static bool TryConvertOffsets(this string[] tokens, out float[] result)
        {
            var offsets = new List<float>();

            foreach (var token in tokens)
            {
                if (TryConvertOffset(token, out var offset))
                    offsets.Add(offset);
            }

            result = offsets.ToArray();
            return result.Length > 0;
        }
    }
}
