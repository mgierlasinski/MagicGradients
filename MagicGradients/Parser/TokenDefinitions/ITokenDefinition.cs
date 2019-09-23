namespace MagicGradients.Parser.TokenDefinitions
{
    public interface ITokenDefinition
    {
        bool IsMatch(string token);

        void Parse(CssReader reader, LinearGradientBuilder gradientBuilder);
    }
}
