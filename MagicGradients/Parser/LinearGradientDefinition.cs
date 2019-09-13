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

        public void Parse(string token, LinearGradientBuilder gradientBuilder)
        {
            //todo try to replace trim with indexed removing on string builder it will improve performance!!!!
            token = token.TrimStart('(').TrimEnd(')', ',');
            if (!token.EndsWith(")"))
                token = token + ')';
            if (char.IsDigit(token[0]))
            {
                var angleString = string.Concat(token.TakeWhile(x => x != ','));
                if (TryConvertDirectionToAngle(angleString, out var angle))
                {
                    gradientBuilder.AddGradient(angle);
                    //todo check if we remove it correctly
                    token = token.Remove(0, angleString.Length + 1);
                }
                else
                {
                    gradientBuilder.AddGradient(0);
                }
            }

            for (var closeIndex = token.IndexOf(')') + 1; closeIndex > 0;
                closeIndex = token.IndexOf(')') + 1)
            {
                /*
                for (var i = 0; i < closeIndex; i++)
                {
                  //todo test this way in Benchmark.Net
                  https://www.stevejgordon.co.uk/introduction-to-benchmarking-csharp-code-with-benchmark-dot-net
                }
                */
                var colorString = string.Concat(token.Take(closeIndex));
                token = token.Remove(0, closeIndex);
                try
                {
                    var color = (Color)_colorConverter.ConvertFromInvariantString(colorString);
                    if (string.Concat(token.TakeWhile(x => x != ',' || x != ')'))
                        .TrimEnd(',', ')')
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

                token = token.IndexOf(',') > -1
                    ? token.Remove(0, token.IndexOf(',') + 1)
                    : string.Empty;
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
