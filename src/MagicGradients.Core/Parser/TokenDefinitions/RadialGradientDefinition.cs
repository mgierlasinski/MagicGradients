using MagicGradients.Builder;
using System;

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
            
            var (hasShape, shape) = GetShape(internalReader);
            var (hasStretch, stretch) = GetStretch(internalReader);
            var (hasRadius, radius) = GeRadius(internalReader, shape);
            var (hasPos, position) = GetPosition(internalReader);

            builder.UseBuilder(new RadialGradientBuilder
            {
                Center = position,
                Shape = shape,
                Stretch = stretch,
                Radius = radius,
                IsRepeating = isRepeating
            });

            if (!hasShape && !hasStretch && !hasRadius && !hasPos)
            {
                reader.Rollback();
            }
        }

        private (bool, RadialGradientShape) GetShape(CssReader reader)
        {
            if (reader.CanRead)
            {
                var token = reader.Read().Trim();

                if (Enum.TryParse<RadialGradientShape>(token, true, out var shape))
                {
                    reader.MoveNext();
                    return (true, shape);
                }
            }
            
            return (false, RadialGradientShape.Ellipse);
        }

        private (bool, RadialGradientStretch) GetStretch(CssReader reader)
        {
            if (reader.CanRead)
            {
                var token = reader.Read().Replace("-", "").Trim();

                if (Enum.TryParse<RadialGradientStretch>(token, true, out var stretch))
                {
                    reader.MoveNext();
                    return (true, stretch);
                }
            }

            return (false, RadialGradientStretch.FarthestCorner);
        }

        private (bool, Dimensions) GeRadius(CssReader reader, RadialGradientShape shape)
        {
            if (reader.CanRead)
            {
                var size = Dimensions.Zero;

                if (shape == RadialGradientShape.Circle)
                {
                    var radiusToken = reader.Read();
                    var isRadius = Offset.TryParseWithUnit(radiusToken, out var radius);

                    if (isRadius)
                    {
                        size = new Dimensions(radius, radius);
                        reader.MoveNext();
                    }
                }

                if (shape == RadialGradientShape.Ellipse)
                {
                    var radiusHToken = reader.Read();
                    var radiusVToken = reader.ReadNext();

                    var isRadiusH = Offset.TryParseWithUnit(radiusHToken, out var radiusH);
                    var isRadiusV = Offset.TryParseWithUnit(radiusVToken, out var radiusV);

                    if (isRadiusH && isRadiusV)
                    {
                        size = new Dimensions(radiusH, radiusV);
                        reader.MoveNext();
                    }
                    else
                    {
                        // Revert radiusVToken
                        reader.Rollback();
                    }
                }

                if (size != Dimensions.Zero)
                    return (true, size);
            }

            return (false, Dimensions.Zero);
        }

        private (bool, Position) GetPosition(CssReader reader)
        {
            if (reader.CanRead)
            {
                var token = reader.Read().Trim();

                if (token == "at")
                {
                    var tokenX = reader.ReadNext();
                    var tokenY = reader.ReadNext();

                    var isPosX = Offset.TryParseWithUnit(tokenX, out var posX);
                    var isPosY = Offset.TryParseWithUnit(tokenY, out var posY);

                    var direction = Vector2.Zero;

                    if (!isPosX && !string.IsNullOrEmpty(tokenX))
                    {
                        direction.SetNamedDirection(tokenX);
                    }

                    if (!isPosY && !string.IsNullOrEmpty(tokenY))
                    {
                        direction.SetNamedDirection(tokenY);
                    }
                    
                    var center = new Position(
                        isPosX ? posX : Offset.Prop((direction.X + 1) / 2),
                        isPosY ? posY : Offset.Prop((direction.Y + 1) / 2));

                    return (true, center);
                }
            }

            return (false, Position.Prop(0.5, 0.5));
        }
    }
}
