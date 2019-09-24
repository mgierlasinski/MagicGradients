namespace MagicGradients.Parser.TokenDefinitions
{
    public class LinearGradientDefinition : ITokenDefinition
    {
        public bool IsMatch(string token) => token == CssToken.LinearGradient;

        public void Parse(CssReader reader, LinearGradientBuilder builder)
        {
            if (TryConvertDegreeToAngle(reader.ReadNext(), out var angle))
            {
                builder.AddGradient(angle);
            }
            else
            {
                builder.AddGradient(0);
                reader.Rollback();
            }
        }

        internal bool TryConvertDegreeToAngle(string token, out int angle)
        {
            if (token.Contains("deg"))
            {
                var degreeStr = token.Replace("deg", "");

                if (int.TryParse(degreeStr, out var degree))
                {
                    angle = (180 + degree) % 360;
                    return true;
                }
            }

            angle = 0;
            return false;
        }
    }
}
