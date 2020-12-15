using MagicGradients.Builder;
using MagicGradients.Xaml;
using System;
using Xamarin.Forms;
using static MagicGradients.RadialGradientFlags;

namespace MagicGradients.Parser.TokenDefinitions
{
    public class RadialGradientDefinition : ITokenDefinition
    {
        protected OffsetTypeConverter OffsetConverter { get; } = new OffsetTypeConverter();

        public bool IsMatch(string token) => 
            token == CssToken.RadialGradient || 
            token == CssToken.RepeatingRadialGradient;

        public void Parse(CssReader reader, GradientBuilder builder)
        {
            var isRepeating = reader.Read().Trim() == CssToken.RepeatingRadialGradient;
            var token = reader.ReadNext().Trim();
            var internalReader = new CssReader(token, ' ');

            var flags = None;

            var hasShape = TryGetShape(internalReader, out var shape);
            var hasSize = TryGetSize(internalReader, out var size);
            var (hasRadius, radius) = GeRadius(internalReader, shape, ref flags);
            var hasPos = TryGetPosition(internalReader, out var position, ref flags);

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

        private bool TryGetShape(CssReader reader, out RadialGradientShape result)
        {
            if (reader.CanRead)
            {
                var token = reader.Read().Trim();

                if (Enum.TryParse<RadialGradientShape>(token, true, out var shape))
                {
                    reader.MoveNext();
                    result = shape;
                    return true;
                }
            }

            result = RadialGradientShape.Ellipse;
            return false;
        }

        private bool TryGetSize(CssReader reader, out RadialGradientSize result)
        {
            if (reader.CanRead)
            {
                var token = reader.Read().Replace("-", "").Trim();

                if (Enum.TryParse<RadialGradientSize>(token, true, out var shapeSize))
                {
                    reader.MoveNext();
                    result = shapeSize;
                    return true;
                }
            }

            result = RadialGradientSize.FarthestCorner;
            return false;
        }

        private (bool, Size) GeRadius(CssReader reader, RadialGradientShape shape, ref RadialGradientFlags flags)
        {
            if (reader.CanRead)
            {
                var size = Dimensions.Zero;

                if (shape == RadialGradientShape.Circle)
                {
                    var radiusToken = reader.Read();
                    var isRadius = OffsetConverter.TryExtractOffset(radiusToken, out var radius);

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

                    var isRadiusH = OffsetConverter.TryExtractOffset(radiusHToken, out var radiusH);
                    var isRadiusV = OffsetConverter.TryExtractOffset(radiusVToken, out var radiusV);

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

        private bool TryGetPosition(CssReader reader, out Point pResult, ref RadialGradientFlags flags)
        {
            if (reader.CanRead)
            {
                var token = reader.Read().Trim();

                if (token == "at")
                {
                    var tokenX = reader.ReadNext();
                    var tokenY = reader.ReadNext();

                    var isPosX = OffsetConverter.TryExtractOffset(tokenX, out var posX);
                    var isPosY = OffsetConverter.TryExtractOffset(tokenY, out var posY);

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

                    pResult = center;
                    return true;
                }
            }

            pResult = new Point(0.5, 0.5);
            FlagsHelper.Set(ref flags, PositionProportional);
            return false;
        }
    }
}
