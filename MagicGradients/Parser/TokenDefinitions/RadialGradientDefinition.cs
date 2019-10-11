using System;
using Xamarin.Forms;

namespace MagicGradients.Parser.TokenDefinitions
{
    public class RadialGradientDefinition : ITokenDefinition
    {
        public bool IsMatch(string token) => 
            token == CssToken.RadialGradient || 
            token == CssToken.RepeatingRadialGradient;

        public void Parse(CssReader reader, GradientBuilder builder)
        {
            var isRepeating = reader.Read().Trim() == CssToken.RepeatingRadialGradient;

            var token = reader.ReadNext().Trim();
            var internalReader = new CssReader(token, ' ');
            
            var shape = GetShape(internalReader);
            var shapeSize = GetShapeSize(internalReader);
            var position = GetPosition(internalReader);
            var flags = GetFlags(position);

            builder.AddRadialGradient(position, shape, shapeSize, flags, isRepeating);
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

        private RadialGradientSize GetShapeSize(CssReader reader)
        {
            if (reader.CanRead)
            {
                var token = reader.Read().Replace("-", "").Trim();

                if (Enum.TryParse<RadialGradientSize>(token, true, out var shapeSize))
                {
                    reader.MoveNext();
                    return shapeSize;
                }
            }

            return RadialGradientSize.FarthestCorner;
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

                    var isPosX = tokenX.TryConvertOffset(out var posX);
                    var isPosY = tokenY.TryConvertOffset(out var posY);

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

        private RadialGradientFlags GetFlags(Point position)
        {
            var flags = RadialGradientFlags.None;

            if (position.X <= 1)
            {
                flags |= RadialGradientFlags.XProportional;
            }

            if (position.Y <= 1)
            {
                flags |= RadialGradientFlags.YProportional;
            }

            return flags;
        }
    }
}
