using System;
using Xamarin.Forms;

namespace MagicGradients.Parser
{
    public class ColorRgbDefinition : ITokenDefinition
    {
        public bool IsMatch(string token) =>
            token == CssToken.Rgb ||
            token == CssToken.Rgba;

        public void Parse(CssReader reader, LinearGradientBuilder builder)
        {
            var token = reader.Read();

            var r = reader.ReadNext().ToByte();
            var g = reader.ReadNext().ToByte();
            var b = reader.ReadNext().ToByte();
            var a = token == CssToken.Rgba ? ConvertToAlpha(reader.ReadNext()) : byte.MaxValue;

            var color = Color.FromRgba(r, g, b, a);

            if (reader.ReadNext().TryConvertPercentToOffset(out var offset))
            {
                builder.AddStop(color, offset);
            }
            else
            {
                builder.AddStop(color);
                reader.Rollback();
            }
        }

        public byte ConvertToAlpha(string token) => (byte)Math.Round(byte.MaxValue * token.ToDouble());
    }
}
