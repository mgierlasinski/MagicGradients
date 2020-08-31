using System;
using Xamarin.Forms.Xaml;

namespace MagicGradients
{
    [TypeConversion(typeof(Dimensions))]
    public class DimensionsTypeConverter : OffsetTypeConverter
    {
        public override object ConvertFromInvariantString(string value)
        {
            if (string.IsNullOrEmpty(value))
                return Dimensions.Zero;

            value = value.Trim();

            var dim = value.Split(',', ' ');
            if (dim.Length == 2)
            {
                return new Dimensions(
                    (Offset)base.ConvertFromInvariantString(dim[0]),
                    (Offset)base.ConvertFromInvariantString(dim[1]));
            }

            throw new InvalidOperationException($"Cannot convert \"{value}\" into {typeof(Dimensions)}");
        }
    }
}
