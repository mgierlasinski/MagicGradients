using MagicGradients;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace Playground.Converters
{
    public class DimensionsToDoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var dim = (Dimensions)value;
            return dim.Width.Value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Dimensions.Prop((double)value, (double)value);
        }
    }
}
