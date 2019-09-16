using MagicGradients.Parser.Extensions;
using MagicGradients.Parser.Readers;
using Xamarin.Forms;

namespace MagicGradients.Parser.TokenDefinitions
{
    public class ColorHslDefinition : ITokenDefinition
    {
        public bool IsMatch(string token) =>
            token == CssToken.Hsl ||
            token == CssToken.Hsla;

        public void Parse(CssNativeReader reader, LinearGradientBuilder gradientBuilder)
        {
            var token = reader.Read();

            var h = reader.ReadNext().ToDouble();
            var s = ParsePercent(reader.ReadNext());
            var l = ParsePercent(reader.ReadNext());
            var a = token == CssToken.Hsla ? reader.ReadNext().ToDouble() : 1d;

            var color = Color.FromHsla(h, s, l, a);

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

        private double ParsePercent(string token) => token.Replace("%", "").ToDouble();
    }
}
