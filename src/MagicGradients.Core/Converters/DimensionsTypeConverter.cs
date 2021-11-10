using System;
using System.ComponentModel;
using System.Globalization;

namespace MagicGradients.Converters
{
    public class DimensionsTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
            => sourceType == typeof(string);

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
            => destinationType == typeof(string);

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var valueStr = value?.ToString()?.Trim();

            if (string.IsNullOrEmpty(valueStr))
                return Dimensions.Zero;

            var dim = valueStr.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (dim.Length == 1)
            {
                return new Dimensions(Offset.Parse(dim[0], OffsetType.Absolute));
            }

            if (dim.Length == 2)
            {
                return new Dimensions(
                    Offset.Parse(dim[0], OffsetType.Absolute),
                    Offset.Parse(dim[1], OffsetType.Absolute));
            }

            throw new InvalidOperationException($"Cannot convert \"{value}\" into {typeof(Dimensions)}");
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (value is Dimensions dim)
                return $"{dim.Width.ToStringWithUnit()} {dim.Height.ToStringWithUnit()}";

            throw new NotSupportedException();
        }
    }
}
