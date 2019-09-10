namespace MagicGradients.Parser
{
    public class LinearGradientDefinition : ITokenDefinition
    {
        public bool IsMatch(string token) => token == CssToken.LinearGradient;

        public void Parse(CssReader reader, LinearGradientBuilder builder)
        {
            if (TryConvertDirectionToAngle(reader.ReadNext(), out var angle))
            {
                builder.AddGradient(angle);
            }
            else
            {
                builder.AddGradient(0);
                reader.Rollback();
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
