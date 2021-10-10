using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MagicGradients.Xaml
{
    [TypeConversion(typeof(Dimensions))]
    public class DimensionsTypeConverter : TypeConverter
    {
        public override object ConvertFromInvariantString(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return Dimensions.Zero;

            value = value.Trim();

            var dim = value.Split(new []{',', ' '}, StringSplitOptions.RemoveEmptyEntries);

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
    }
}
