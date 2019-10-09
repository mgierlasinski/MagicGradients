using System;
using System.Globalization;

namespace MagicGradients.Parser.TokenDefinitions
{
    public class LinearGradientDefinition : ITokenDefinition
    {
        public bool IsMatch(string token) =>
            token == CssToken.LinearGradient ||
            token == CssToken.RepeatingLinearGradient;

        public void Parse(CssReader reader, GradientBuilder builder)
        {
            var repeating = reader.Read() == CssToken.RepeatingLinearGradient;
            var direction = reader.ReadNext().Trim();

            if (TryConvertDegreeToAngle(direction, out var degreeToAngle))
            {
                builder.AddLinearGradient(degreeToAngle, repeating);
            }
            else if (TryConvertNamedDirectionToAngle(direction, out var directionToAngle))
            {
                builder.AddLinearGradient(directionToAngle, repeating);
            }
            else
            {
                builder.AddLinearGradient(0, repeating);
                reader.Rollback();
            }
        }

        internal bool TryConvertDegreeToAngle(string token, out double angle)
        {
            if (token.EndsWith("deg", StringComparison.OrdinalIgnoreCase))
            {
                var index = token.LastIndexOf("deg", StringComparison.OrdinalIgnoreCase);
                var degreesStr = token.Substring(0, index);

                if (double.TryParse(degreesStr, NumberStyles.Any, CultureInfo.InvariantCulture, out var degrees))
                {
                    angle = CssHelpers.FromDegrees(degrees);
                    return true;
                }
            }

            angle = 0;
            return false;
        }

        internal bool TryConvertNamedDirectionToAngle(string token, out double angle)
        {
            var parts = token.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length > 1 && parts[0] == "to")
            {
                var defaultVector = Vector2.Down;   // By default gradient is drawn top-down
                var directionVector = Vector2.Zero;

                for (var i = 1; i < parts.Length; i++)
                {
                    directionVector.SetNamedDirection(parts[i]);
                }

                angle = Vector2.Angle(ref defaultVector, ref directionVector);

                // We start rotation from Vector(0, -1) clockwise
                // If gradient ends in I or II quarter of coordinate system
                // we use angle to calculate actual rotation

                if (directionVector.X > 0)
                {
                    angle = 360 - angle;
                }

                return true;
            }

            angle = 0;
            return false;
        }
    }
}
