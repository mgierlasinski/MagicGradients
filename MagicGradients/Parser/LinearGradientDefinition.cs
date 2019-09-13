using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace MagicGradients.Parser
{
    public class LinearGradientDefinition : ITokenDefinition
    {
        private readonly ColorTypeConverter _colorConverter = new ColorTypeConverter();

        public bool IsMatch(string token) => token == CssToken.LinearGradient;

        public void Parse(CssReader reader, LinearGradientBuilder gradientBuilder)
        {
            if (TryConvertDirectionToAngle(reader.ReadNext(), out var angle))
            {
                gradientBuilder.AddGradient(angle);
            }
            else
            {
                gradientBuilder.AddGradient(0);
                reader.Rollback();
            }
        }

        public void Parse(in string token, LinearGradientBuilder gradientBuilder)
        {
            var tokenIndex = token.StartsWith("(") ? 1 : 0;
            if (char.IsDigit(token[tokenIndex]))
            {
                var angleString = token.Substring(tokenIndex, token.IndexOf(',', tokenIndex) - 1);
                if (TryConvertDirectionToAngle(angleString, out var angle))
                {
                    gradientBuilder.AddGradient(angle);
                    tokenIndex += angleString.Length + 1;
                }
                else
                {
                    gradientBuilder.AddGradient(0);
                }
            }

            for (var closeIndex = token.IndexOf(')', tokenIndex) + 1 - tokenIndex; closeIndex > -1;)
            {
                var colorString = token.Substring(tokenIndex, closeIndex);
                tokenIndex += closeIndex;
                try
                {
                    var color = (Color)_colorConverter.ConvertFromInvariantString(colorString);
                    if (token.Substring(tokenIndex, token.IndexOfAny(new[] { ',', ')' }, tokenIndex) - tokenIndex)
                        .TryConvertPercentToOffset(out var offset))
                    {
                        gradientBuilder.AddStop(color, offset);
                    }
                    else
                    {
                        gradientBuilder.AddStop(color);
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }

                var commaIndex = token.IndexOf(',', tokenIndex);

                if (commaIndex > -1)
                {
                    tokenIndex = commaIndex;
                    closeIndex = token.IndexOf(')', tokenIndex) + 1 - tokenIndex;
                }
                else
                {
                    closeIndex = -1;
                }
            }
        }

        private bool TryConvertDirectionToAngle(string token, out int result)
        {
            if (token.Contains("deg"))
            {
                var degree = token.Replace("deg", "");

                if (int.TryParse(degree, out var angle))
                {
                    result = (180 + angle) % 360;
                    return true;
                }
            }

            result = 0;
            return false;
        }
    }
}
