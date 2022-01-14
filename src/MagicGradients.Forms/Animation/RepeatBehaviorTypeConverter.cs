using System;
using System.ComponentModel;
using System.Globalization;

namespace MagicGradients.Forms.Animation
{
    public class RepeatBehaviorTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
            => sourceType == typeof(string);

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var valueStr = value?.ToString()?.Trim();

            if (!string.IsNullOrEmpty(valueStr))
            {
                if (Offset.TryExtractNumber(valueStr, "x", out var count))
                {
                    return new RepeatBehavior(RepeatBehaviorType.Count, (int)count);
                }
                if (valueStr.Equals("Forever", StringComparison.OrdinalIgnoreCase))
                {
                    return new RepeatBehavior(RepeatBehaviorType.Forever, 0);
                }
            }

            throw new InvalidOperationException($"Cannot convert \"{value}\" into {typeof(RepeatBehavior)}");
        }
    }
}
