using System;
using System.ComponentModel;
using System.Globalization;

namespace MagicGradients.Converters
{
    public class OffsetTypeConverter : TypeConverter
    { 
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
            => sourceType == typeof(string);
        
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
            => destinationType == typeof(string);

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return Offset.Parse(value?.ToString(), OffsetType.Proportional);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (value is Offset offset)
                return offset.ToStringWithUnit();

            throw new NotSupportedException();
        }
    }
}
