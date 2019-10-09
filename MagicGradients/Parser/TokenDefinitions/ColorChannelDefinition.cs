using System;
using System.Text;
using Xamarin.Forms;

namespace MagicGradients.Parser.TokenDefinitions
{
    public class ColorChannelDefinition : ColorDefinition, ITokenDefinition
    {
        public bool IsMatch(string token) =>
            token.Equals(CssToken.Rgb, StringComparison.OrdinalIgnoreCase) ||
            token.Equals(CssToken.Rgba, StringComparison.OrdinalIgnoreCase) ||
            token.Equals(CssToken.Hsl, StringComparison.OrdinalIgnoreCase) ||
            token.Equals(CssToken.Hsla, StringComparison.OrdinalIgnoreCase);

        public void Parse(CssReader reader, GradientBuilder builder)
        {
            var color = (Color)ColorConverter.ConvertFromInvariantString(GetColorString(reader));

            var parts = reader.ReadNext().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 0)
            {
                builder.AddStop(color);
            }

            for (var i = 0; i < parts.Length; i++)
            {
                if (TryConvertPercentToOffset(parts[i], out var offset))
                {
                    builder.AddStop(color, offset);
                }
                else
                {
                    builder.AddStop(color);
                    if (parts.Length == 1)
                    {
                        reader.Rollback();
                    }
                }
            }
        }

        internal string GetColorString(CssReader reader)
        {
            var token = reader.Read().Trim();
            var builder = new StringBuilder(token);

            builder.Append('(');
            builder.Append(reader.ReadNext());
            builder.Append(',');
            builder.Append(reader.ReadNext());
            builder.Append(',');
            builder.Append(reader.ReadNext());

            if (token == CssToken.Rgba || token == CssToken.Hsla)
            {
                builder.Append(',');
                builder.Append(reader.ReadNext());
            }

            builder.Append(')');

            return builder.ToString();
        }
    }
}
