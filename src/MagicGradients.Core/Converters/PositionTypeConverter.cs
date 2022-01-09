using System;
using System.ComponentModel;
using System.Globalization;

namespace MagicGradients.Converters
{
    public class PositionTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
            => sourceType == typeof(string);

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
            => destinationType == typeof(string);

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var valueStr = value?.ToString()?.Trim();

            if (string.IsNullOrEmpty(valueStr))
                return Position.Zero;

            var pos = valueStr.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (pos.Length == 2)
            {
                return new Position(
                    Offset.Parse(pos[0], OffsetType.Absolute),
                    Offset.Parse(pos[1], OffsetType.Absolute));
            }

            throw new InvalidOperationException($"Cannot convert \"{value}\" into {typeof(Position)}");
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (value is Position pos)
                return $"{pos.X.ToStringWithUnit()} {pos.Y.ToStringWithUnit()}";

            throw new NotSupportedException();
        }
    }
}
