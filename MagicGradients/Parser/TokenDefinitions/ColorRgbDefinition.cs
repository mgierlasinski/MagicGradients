using System;
using MagicGradients.Parser.Extensions;
using MagicGradients.Parser.Readers;
using Xamarin.Forms;

namespace MagicGradients.Parser.TokenDefinitions
{
    public class ColorRgbDefinition : ITokenDefinition
    {
        public bool IsMatch(string token) =>
            token == CssToken.Rgb ||
            token == CssToken.Rgba;

        public void Parse(CssNativeReader reader, LinearGradientBuilder gradientBuilder)
        {
            var token = reader.Read();

            var r = reader.ReadNext().ParseColorValue(255, true);
            var g = reader.ReadNext().ParseColorValue(255, true);
            var b = reader.ReadNext().ParseColorValue(255, true);
            var a = token == CssToken.Rgba ? reader.ReadNext().ToDouble() : 1;

            var color = Color.FromRgba(r, g, b, a);

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

        public byte ConvertToAlpha(string token) => (byte)Math.Round(byte.MaxValue * token.ToDouble());
    }
}
