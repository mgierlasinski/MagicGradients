using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MagicGradients.Xaml
{
    [TypeConversion(typeof(IGradientSource))]
    public class CssGradientSourceTypeConverter : TypeConverter
    {
        public override object ConvertFromInvariantString(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new InvalidOperationException($"Cannot convert \"{value}\" into {typeof(CssGradientSource)}");

            return new CssGradientSource
            {
                Stylesheet = value
            };
        }
    }
}
