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

        public void Parse(CssReader reader, LinearGradientBuilder builder)
        {
            var color = (Color)ColorConverter.ConvertFromInvariantString(GetColorString(reader));

            if (TryConvertPercentToOffset(reader.ReadNext(), out var offset))
            {
                builder.AddStop(color, offset);
            }
            else
            {
                builder.AddStop(color);
                reader.Rollback();
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
