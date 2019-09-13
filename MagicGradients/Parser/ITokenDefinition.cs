namespace MagicGradients.Parser
{
    public interface ITokenDefinition
    {
        bool IsMatch(string token);

        void Parse(CssReader reader, LinearGradientBuilder gradientBuilder);
    }
}
