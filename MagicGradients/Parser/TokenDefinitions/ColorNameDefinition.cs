using System;
using Xamarin.Forms;

namespace MagicGradients.Parser.TokenDefinitions
{
    public class ColorNameDefinition : ColorDefinition, ITokenDefinition
    {
        public bool IsMatch(string token)
        {
            var parts = token.Split('.');
            return parts.Length == 1 || parts.Length == 2 && parts[0] == "Color";
        }

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