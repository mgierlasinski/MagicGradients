using System;
using Xamarin.Forms;

namespace MagicGradients.Parser.TokenDefinitions
{
    public class RadialGradientDefinition : ITokenDefinition
    {
        public bool IsMatch(string token) => token == CssToken.RadialGradient;

        public void Parse(CssReader reader, GradientBuilder builder)
        {
            var token = reader.ReadNext().Trim();
            var internalReader = new CssReader(token, new[] { ' ' });
            
            var shape = GetShape(internalReader);
            var shapeSize = GetShapeSize(internalReader);
            var position = GetPosition(internalReader);

            builder.AddRadialGradient(position, shape, shapeSize);
        }

        private RadialGradientShape GetShape(CssReader reader)
        {
            if (reader.CanRead)
            {
                var token = reader.Read().Trim();

                if (Enum.TryParse<RadialGradientShape>(token, true, out var shape))
                {
                    reader.MoveNext();
                    return shape;
                }
            }

            return RadialGradientShape.Ellipse;
        }

        private RadialGradientShapeSize GetShapeSize(CssReader reader)
        {
            if (reader.CanRead)
            {
                var token = reader.Read().Replace("-", "").Trim();

                if (Enum.TryParse<RadialGradientShapeSize>(token, true, out var shapeSize))
                {
                    reader.MoveNext();
                    return shapeSize;
                }
            }

            return RadialGradientShapeSize.FarthestCorner;
        }

        private Point GetPosition(CssReader reader)
        {
            if (reader.CanRead)
            {
                var token = reader.Read().Trim();

                if (token == "at")
                {
                    var tokenX = reader.ReadNext();
                    var tokenY = reader.ReadNext();

                    var isPosX = tokenX.TryConvertPercentToProportion(out var posX);
                    var isPosY = tokenY.TryConvertPercentToProportion(out var posY);

                    var direction = Vector2.Zero;

                    if (!isPosX && !string.IsNullOrEmpty(tokenX))
                    {
                        direction.SetNamedDirection(tokenX);
                    }

                    if (!isPosY && !string.IsNullOrEmpty(tokenY))
                    {
                        direction.SetNamedDirection(tokenY);
                    }

                    return new Point(
                        isPosX ? posX : (direction.X + 1) / 2, 
                        isPosY ? posY : (direction.Y + 1) / 2);
                }
            }

            return new Point(0.5, 0.5);
        }
    }
}
