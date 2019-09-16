using MagicGradients.Parser.Extensions;
using MagicGradients.Parser.Readers;

namespace MagicGradients.Parser.TokenDefinitions
{
    public class LinearNativeGradientDefinition : ITokenDefinition
    {
        public bool IsMatch(string token) => token == CssToken.LinearGradient;

        public void Parse(CssNativeReader reader, LinearGradientBuilder gradientBuilder)
        {
            if (reader.ReadNext().TryConvertDirectionToAngle(out var angle))
            {
                gradientBuilder.AddGradient(angle);
            }
            else
            {
                gradientBuilder.AddGradient(0);
                reader.Rollback();
            }
        }
    }
}
