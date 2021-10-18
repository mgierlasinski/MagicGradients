using MagicGradients.Parser;
using System;
using System.ComponentModel;
using System.Globalization;

namespace MagicGradients.Converters
{
    public class CssGradientSourceTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
            => sourceType == typeof(string);

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var valueStr = value?.ToString();

            if (string.IsNullOrEmpty(valueStr))
                throw new InvalidOperationException($"Cannot convert \"{value}\" into {typeof(IGradientSource)}");

            return new CssGradientParserSource(valueStr);
        }
    }
}
