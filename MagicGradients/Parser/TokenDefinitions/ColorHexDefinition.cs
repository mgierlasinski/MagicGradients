using System;
using Xamarin.Forms;

namespace MagicGradients.Parser.TokenDefinitions
{
    public class ColorHexDefinition : ColorDefinition, ITokenDefinition
    {
        public bool IsMatch(string token) => token.StartsWith("#", StringComparison.Ordinal);

        public void Parse(CssReader reader, GradientBuilder builder)
        {
            var parts = reader.Read().Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);

            var color = parts.Length > 0 
                ? (Color)ColorConverter.ConvertFromInvariantString(parts[0]) 
                : Color.Black;

            if (parts.Length > 1 && TryConvertPercentToOffset(parts[1], out var offset))
            {
                builder.AddStop(color, offset);
            }
            else
            {
                builder.AddStop(color);
            }
        }
    }
}
