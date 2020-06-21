using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MagicGradients
{
    [TypeConversion(typeof(Offset))]
    public class OffsetTypeConverter : TypeConverter
    {
        public override object ConvertFromInvariantString(string value)
        {
            if (value != null)
            {
                value = value.Trim();

                if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var d))
                {
                    return new Offset(d, OffsetType.Proportional);
                }

                if (TryExtractOffset(value, out var res))
                {
                    return res;
                }
            }

            throw new InvalidOperationException($"Cannot convert \"{value}\" into {typeof(Offset)}");
        }

        public bool TryExtractOffset(string token, out Offset result)
        {
            if (token != null)
            {
                if (token.TryExtractNumber("%", out var percent))
                {
                    var value = Math.Min(percent / 100, 1f); // No bigger than 1
                    result =  new Offset(value, OffsetType.Proportional);
                    return true;
                }

                if (token.TryExtractNumber("px", out var pixels))
                {
                    result = new Offset(pixels, OffsetType.Absolute);
                    return true;
                }
            }

            result = Offset.Zero;
            return false;
        }
    }
}
