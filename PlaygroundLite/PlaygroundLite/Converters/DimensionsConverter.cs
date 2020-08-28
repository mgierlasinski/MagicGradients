using MagicGradients;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace PlaygroundLite.Converters
{
    public class DimensionsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((Dimensions)value).Width.Value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Dimensions.Prop((double)value, (double)value);
        }
    }
}
