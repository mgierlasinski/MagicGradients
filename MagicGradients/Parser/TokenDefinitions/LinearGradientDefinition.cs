namespace MagicGradients.Parser.TokenDefinitions
{
    public class LinearGradientDefinition : ITokenDefinition
    {
        public bool IsMatch(string token) => token == CssToken.LinearGradient;

        public void Parse(CssReader reader, LinearGradientBuilder gradientBuilder)
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
