using MagicGradients.Parser;
using System;
using Xamarin.Forms;

namespace MagicGradients.Animation
{
    [Xamarin.Forms.Xaml.TypeConversion(typeof(RepeatBehavior))]
    public class RepeatBehaviorTypeConverter : TypeConverter
    {
        public override object ConvertFromInvariantString(string value)
        {
            if (value != null)
            {
                value = value.Trim();
                if (value.TryExtractNumber("x", out var count))
                {
                    return new RepeatBehavior(RepeatBehaviorType.Count, (int)count);
                }
                if (value.Equals("Forever", StringComparison.OrdinalIgnoreCase))
                {
                    return new RepeatBehavior(RepeatBehaviorType.Forever, 0);
                }
            }

            throw new InvalidOperationException($"Cannot convert \"{value}\" into {typeof(RepeatBehavior)}");
        }
    }
}
