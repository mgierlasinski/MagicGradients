using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MagicGradients.Masks
{
    [TypeConversion(typeof(Corners))]
    public class CornersTypeConverter : TypeConverter
    {
        public override object ConvertFromInvariantString(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return Corners.Zero;

            value = value.Trim();

            var dim = value.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (dim.Length == 1)
            {
                return new Corners(new Dimensions(Offset.Parse(dim[0], OffsetType.Absolute)));
            }

            if (dim.Length == 2)
            {
                return new Corners(
                    new Dimensions(Offset.Parse(dim[0], OffsetType.Absolute)), 
                    new Dimensions(Offset.Parse(dim[0], OffsetType.Absolute)), 
                    new Dimensions(Offset.Parse(dim[1], OffsetType.Absolute)), 
                    new Dimensions(Offset.Parse(dim[1], OffsetType.Absolute)));
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
    }
}
