using System;
using System.ComponentModel;
using System.Globalization;

namespace MagicGradients.Converters
{
    public class BackgroundRepeatTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) 
            => sourceType == typeof(string);

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var valueStr = value?.ToString()?.Trim().Replace("-", "");

            if (!string.IsNullOrEmpty(valueStr) && Enum.TryParse<BackgroundRepeat>(valueStr, true, out var result))
            {
                return result;
            }

            throw new InvalidOperationException($"Cannot convert \"{value}\" into {typeof(BackgroundRepeat)}");
        }
    }
}
