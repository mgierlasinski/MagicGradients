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
            
            var shape = GetShape(internalReader);
            var shapeSize = GetShapeSize(internalReader);
            var (position, flags) = GetPositionWithFlags(internalReader);

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

        private (Point, RadialGradientFlags) GetPositionWithFlags(CssReader reader)
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

                    var flags = None;

                    if(!isPosX || posX.Type == OffsetType.Proportional)
                        flags |= XProportional;

                    if (!isPosY || posY.Type == OffsetType.Proportional)
                        flags |= YProportional;

                    var center = new Point(
                        isPosX ? posX.Value : (direction.X + 1) / 2,
                        isPosY ? posY.Value : (direction.Y + 1) / 2);

                    return (center, flags);
                }
            }

            return (new Point(0.5, 0.5), PositionProportional);
        }
    }
}
