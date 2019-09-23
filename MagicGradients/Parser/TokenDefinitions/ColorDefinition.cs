using System;
using System.Text;
using Xamarin.Forms;

namespace MagicGradients.Parser.TokenDefinitions
{
    public class ColorDefinition : ITokenDefinition
    {
        private readonly ColorTypeConverter _colorConverter = new ColorTypeConverter();

        public bool IsMatch(string token) =>
            token == CssToken.Rgb ||
            token == CssToken.Rgba ||
            token == CssToken.Hsl ||
            token == CssToken.Hsla;

        public void Parse(CssReader reader, LinearGradientBuilder gradientBuilder)
        {
            var color = (Color)_colorConverter.ConvertFromInvariantString(GetColorString(reader));

            if (reader.ReadNext().TryConvertPercentToOffset(out var offset))
            {
                gradientBuilder.AddStop(color, offset);
            }
            else
            {
                gradientBuilder.AddStop(color);
                reader.Rollback();
            }
        }

        internal string GetColorString(CssReader reader)
        {
            var token = reader.Read();
            var colorStringBuilder = new StringBuilder();

            colorStringBuilder.Append(token);
            colorStringBuilder.Append('(');
            colorStringBuilder.Append(reader.ReadNext());
            colorStringBuilder.Append(',');
            colorStringBuilder.Append(reader.ReadNext());
            colorStringBuilder.Append(',');
            colorStringBuilder.Append(reader.ReadNext());

            if (token == CssToken.Rgba || token == CssToken.Hsla)
            {
                colorStringBuilder.Append(',');
                colorStringBuilder.Append(reader.ReadNext().ToDouble());
                colorStringBuilder.Append(')');
            }
            else
            {
                colorStringBuilder.Append(')');
            }

            return colorStringBuilder.ToString();
        }
    }
}
