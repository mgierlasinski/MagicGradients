using System;
using System.ComponentModel;
using System.Globalization;

namespace MagicGradients.Converters
{
    public class CornersTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
            => sourceType == typeof(string);

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
            => destinationType == typeof(string);

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var valueStr = value?.ToString()?.Trim();

            if (string.IsNullOrWhiteSpace(valueStr))
                return Corners.Zero;

            var dim = valueStr.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (dim.Length == 1)
            {
                return new Corners(new Dimensions(Offset.Parse(dim[0], OffsetType.Absolute)));
            }

            if (dim.Length == 2)
            {
                var top = new Dimensions(Offset.Parse(dim[0], OffsetType.Absolute));
                var bottom = new Dimensions(Offset.Parse(dim[1], OffsetType.Absolute));

                return new Corners(top, top, bottom, bottom);
            }

            if (dim.Length == 4)
            {
                return new Corners(
                    new Dimensions(Offset.Parse(dim[0], OffsetType.Absolute)),
                    new Dimensions(Offset.Parse(dim[1], OffsetType.Absolute)),
                    new Dimensions(Offset.Parse(dim[2], OffsetType.Absolute)),
                    new Dimensions(Offset.Parse(dim[3], OffsetType.Absolute)));
            }

            throw new InvalidOperationException($"Cannot convert \"{value}\" into {typeof(Corners)}");
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (value is Corners c)
                return $"{c.TopLeft.Width.ToStringWithUnit()} {c.TopRight.Width.ToStringWithUnit()} {c.BottomRight.Width.ToStringWithUnit()} {c.BottomLeft.Width.ToStringWithUnit()}";

            throw new NotSupportedException();
        }
    }
}
