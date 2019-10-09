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

            var color = (Color)ColorConverter.ConvertFromInvariantString(parts[0]);

            if (parts.TryConvertOffsets(out var offsets))
            {
                foreach (var offset in offsets)
                {
                    builder.AddStop(color, offset);
                }
            }
            else
            {
                builder.AddStop(color);
            }
        }
    }
}
