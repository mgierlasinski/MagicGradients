using MagicGradients.Builder;
using Microsoft.Maui.Graphics;
using System;
using static MagicGradients.RadialGradientFlags;

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

            var flags = None;

            var (hasShape, shape) = GetShape(internalReader);
            var (hasSize, size) = GetSize(internalReader);
            var (hasRadius, radius) = GeRadius(internalReader, shape, ref flags);
            var (hasPos, position) = GetPosition(internalReader, ref flags);

            builder.UseBuilder(new RadialGradientBuilder
            {
                Center = position,
                Shape = shape,
                Size = size,
                RadiusX = radius.Width,
                RadiusY = radius.Height,
                Flags = flags,
                IsRepeating = isRepeating
            });

            if (!hasShape && !hasSize && !hasRadius && !hasPos)
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

        private (bool, RadialGradientSize) GetSize(CssReader reader)
        {
            if (reader.CanRead)
            {
                var token = reader.Read().Replace("-", "").Trim();

                if (Enum.TryParse<RadialGradientSize>(token, true, out var shapeSize))
                {
                    reader.MoveNext();
                    return (true, shapeSize);
                }
            }

            return (false, RadialGradientSize.FarthestCorner);
        }

        private (bool, Size) GeRadius(CssReader reader, RadialGradientShape shape, ref RadialGradientFlags flags)
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
                {
                    if (size.Width.Type == OffsetType.Proportional)
                        FlagsHelper.Set(ref flags, WidthProportional);

                    if (size.Height.Type == OffsetType.Proportional)
                        FlagsHelper.Set(ref flags, HeightProportional);

                    return (true, new Size(size.Width.Value, size.Height.Value));
                }
            }

            // Value -1 means undefined for RadialGradientShader
            return (false, new Size(-1, -1));
        }

        private (bool, Point) GetPosition(CssReader reader, ref RadialGradientFlags flags)
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

                    if(!isPosX || posX.Type == OffsetType.Proportional)
                        FlagsHelper.Set(ref flags, XProportional);

                    if (!isPosY || posY.Type == OffsetType.Proportional)
                        FlagsHelper.Set(ref flags, YProportional);

                    var center = new Point(
                        isPosX ? posX.Value : (direction.X + 1) / 2,
                        isPosY ? posY.Value : (direction.Y + 1) / 2);

                    return (true, center);
                }
            }

            FlagsHelper.Set(ref flags, PositionProportional);
            return (false, new Point(0.5, 0.5));
        }
    }
}
