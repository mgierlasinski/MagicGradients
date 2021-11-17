using MagicGradients.Builder;
using Microsoft.Maui.Graphics;
using System;
using System.Linq;

namespace MagicGradients.Parser.TokenDefinitions
{
    public class ColorHexDefinition : ColorDefinition, ITokenDefinition
    {
        public bool IsMatch(string token) => token.StartsWith("#", StringComparison.Ordinal);

        public void Parse(CssReader reader, GradientBuilder builder)
        {
            var parts = reader.Read().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var color = Color.Parse(parts[0]);
            var offsets = GetOffsets(parts);

            if (offsets.Any())
            {
                builder.AddStops(color, offsets);
            }
            else
            {
                builder.AddStop(color);
            }
        }
    }
}
