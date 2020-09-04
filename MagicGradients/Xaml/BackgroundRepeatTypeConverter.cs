using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MagicGradients.Xaml
{
    [TypeConversion(typeof(BackgroundRepeat))]
    public class BackgroundRepeatTypeConverter : TypeConverter
    {
        public override object ConvertFromInvariantString(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                value = value.Trim().Replace("-", "");

                if (Enum.TryParse<BackgroundRepeat>(value, true, out var result))
                {
                    return result;
                }
            }

            throw new InvalidOperationException($"Cannot convert \"{value}\" into {typeof(BackgroundRepeat)}");
        }
    }
}
