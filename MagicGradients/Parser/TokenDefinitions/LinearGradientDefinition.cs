using System;
using System.Globalization;

namespace MagicGradients.Parser.TokenDefinitions
{
    public class LinearGradientDefinition : ITokenDefinition
    {
        public bool IsMatch(string token) => token == CssToken.LinearGradient;

        public void Parse(CssReader reader, LinearGradientBuilder builder)
        {
            var direction = reader.ReadNext().Trim();

            if (TryConvertDegreeToAngle(direction, out var degreeToAngle))
            {
                builder.AddGradient(degreeToAngle);
            }
            else if (TryConvertNamedDirectionToAngle(direction, out var directionToAngle))
            {
                builder.AddGradient(directionToAngle);
            }
            else
            {
                builder.AddGradient(0);
                reader.Rollback();
            }
        }

        internal bool TryConvertDegreeToAngle(string token, out double angle)
        {
            if (token.Contains("deg"))
            {
                var degreesStr = token.Replace("deg", "");

                if(double.TryParse(degreesStr, NumberStyles.Any, CultureInfo.InvariantCulture, out var degrees))
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
            var parts = token.Split(new []{' '}, StringSplitOptions.RemoveEmptyEntries);

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
