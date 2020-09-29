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

            var hasShape = TryGetShape(internalReader, out var shape);
            var hasSize = TryGetSize(internalReader, out var size);
            var hasPos = TryGetPositionWithFlags(internalReader, out var position, out var flags);

            builder.AddRadialGradient(position, shape, size, flags, isRepeating);

            if (!hasShape && !hasSize && !hasPos)
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

        private bool TryGetPositionWithFlags(CssReader reader, out Point pResult, out RadialGradientFlags fResult)
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

                    pResult = center;
                    fResult = flags;
                    return true;
                }
            }

            pResult = new Point(0.5, 0.5);
            fResult = PositionProportional;
            return false;
        }
    }
}
