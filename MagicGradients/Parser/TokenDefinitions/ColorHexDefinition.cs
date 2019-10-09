using System;
using Xamarin.Forms;

namespace MagicGradients.Parser.TokenDefinitions
{
    public class ColorHexDefinition : ColorDefinition, ITokenDefinition
    {
        public bool IsMatch(string token) => token.StartsWith("#", StringComparison.Ordinal);

        public void Parse(CssReader reader, GradientBuilder builder)
        {
            var parts = reader.Read().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            var color = parts.Length > 0
                ? (Color)ColorConverter.ConvertFromInvariantString(parts[0])
                : Color.Black;

            if (parts.Length == 1)
            {
                builder.AddStop(color);
                return;
            }

            for (var i = 1; i < parts.Length; i++)
            {
                if (TryConvertPercentToOffset(parts[i], out var offset))
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
}
