using MagicGradients.Parser.Readers;

namespace MagicGradients.Parser.TokenDefinitions
{
    public interface ITokenDefinition
    {
        bool IsMatch(string token);

        void Parse(CssNativeReader reader, LinearGradientBuilder gradientBuilder);
    }
}
