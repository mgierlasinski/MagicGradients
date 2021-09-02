using MagicGradients.Builder;
using System;
using System.Linq;
using Xamarin.Forms;

namespace MagicGradients.Parser.TokenDefinitions
{
    public class ColorNameDefinition : ColorDefinition, ITokenDefinition
    {
        public bool IsMatch(string token)
        {
            var parts = token.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            return parts.Length == 1 || parts.Length == 2 && parts[0] == "Color";
        }

        public void Parse(CssReader reader, GradientBuilder builder)
        {
            var parts = reader.Read().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var color = GetNamedColor(parts[0]);
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

        private Color GetNamedColor(string colorName)
        {
            // For transparency SkiaSharp needs black color with zero alpha
            return colorName == "transparent" ?
                Color.FromRgba(0, 0, 0, 0) :
                (Color)ColorConverter.ConvertFromInvariantString(colorName);
        }
    }
}