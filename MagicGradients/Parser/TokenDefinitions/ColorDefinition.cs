using System;
using System.Globalization;
using Xamarin.Forms;

namespace MagicGradients.Parser.TokenDefinitions
{
    public abstract class ColorDefinition
    {
        protected ColorTypeConverter ColorConverter { get; } = new ColorTypeConverter();

        internal bool TryConvertPercentToOffset(string token, out float result)
        {
            if (token != null && token.EndsWith("%", StringComparison.OrdinalIgnoreCase))
            {
                var index = token.LastIndexOf("%", StringComparison.OrdinalIgnoreCase);
                var percent = token.Substring(0, index);

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
