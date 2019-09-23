using System;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;

namespace MagicGradients.Parser.TokenDefinitions
{
    public class LinearFormsGradientDefinition
    {
        private readonly ColorTypeConverter _colorConverter = new ColorTypeConverter();

        public void Parse(string token, LinearGradientBuilder gradientBuilder)
        {
            token = token.TrimStart('(').TrimEnd(')', ',');
            if (!token.EndsWith(")"))
            {
                token = token + ')';
            }

            if (char.IsDigit(token[0]))
            {
                var angleString = string.Concat(token.TakeWhile(x => x != ','));
                if (angleString.TryConvertDirectionToAngle(out var angle))
                {
                    gradientBuilder.AddGradient(angle);
                    token = token.Remove(0, angleString.Length + 1);
                }
                else
                {
                    gradientBuilder.AddGradient(0);
                }
            }

            for (var closeIndex = token.IndexOf(')') + 1; closeIndex > 0; closeIndex = token.IndexOf(')') + 1)
            {
                var colorString = string.Concat(token.Take(closeIndex));
                token = token.Remove(0, closeIndex);
                try
                {
                    var color = (Color)_colorConverter.ConvertFromInvariantString(colorString);
                    if (string.Concat(token.TakeWhile(x => x != ',' && x != ')'))
                        .TrimEnd(',', ')').TryConvertPercentToOffset(out var offset))
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
                    //todo add logger!?
                    Debug.WriteLine(e);
                    //todo add options like "ThrowOnErrors?" - that will not log error but throw them to top?
                }

                token = token.IndexOf(',') > -1
                    ? token.Remove(0, token.IndexOf(',') + 1)
                    : string.Empty;
            }
        }
    }
}
