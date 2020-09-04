using System;
using Xamarin.Forms.Xaml;

namespace MagicGradients.Xaml
{
    [TypeConversion(typeof(Dimensions))]
    public class DimensionsTypeConverter : OffsetTypeConverter
    {
        public override object ConvertFromInvariantString(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return Dimensions.Zero;

            value = value.Trim();

            var dim = value.Split(new []{',', ' '}, StringSplitOptions.RemoveEmptyEntries);
            if (dim.Length == 2)
            {
                return new Dimensions(
                    GetOffset(dim[0], OffsetType.Absolute),
                    GetOffset(dim[1], OffsetType.Absolute));
            }

            throw new InvalidOperationException($"Cannot convert \"{value}\" into {typeof(Dimensions)}");
        }
    }
}
