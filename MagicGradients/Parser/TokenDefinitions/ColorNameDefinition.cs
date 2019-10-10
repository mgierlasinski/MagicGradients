using System;
using Xamarin.Forms;

namespace MagicGradients.Parser.TokenDefinitions
{
    public class ColorNameDefinition : ITokenDefinition
    {
        protected ColorTypeConverter ColorConverter { get; } = new ColorTypeConverter();

        public bool IsMatch(string token)
        {
            var parts = token.Split('.');
            return parts.Length == 1 || parts.Length == 2 && parts[0] == "Color";
        }

        public void Parse(CssReader reader, GradientBuilder builder)
        {
            var parts = reader.Read().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var color = (Color)ColorConverter.ConvertFromInvariantString(parts[0]);

            if (parts.TryConvertOffsets(out var offsets))
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