using System;
using System.Globalization;
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

            if (TryConvertPercentToOffset(reader.ReadNext(), out var offset))
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
            var builder = new StringBuilder();

            builder.AppendFormat("{0}({1},{2},{3}", token, 
                reader.ReadNext(), 
                reader.ReadNext(), 
                reader.ReadNext());
            
            if (token == CssToken.Rgba || token == CssToken.Hsla)
            {
                builder.AppendFormat(",{0}", reader.ReadNext());
            }
            
            builder.Append(')');

            return builder.ToString();
        }

        internal bool TryConvertPercentToOffset(string token, out float result)
        {
            if (token != null && token.Contains("%"))
            {
                var percent = token.Replace("%", "");

                if (float.TryParse(percent, NumberStyles.Any, CultureInfo.InvariantCulture, out var value))
                {
                    result = Math.Min(value / 100, 1f); // Make sure no bigger than 1
                    return true;
                }
            }

            result = 0;
            return false;
        }

        internal double ToDouble(string token, double @default = default)
        {
            if (double.TryParse(token, NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
            {
                return result;
            }
            return @default;
        }
    }
}
