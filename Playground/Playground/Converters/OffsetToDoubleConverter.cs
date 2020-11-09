using System;
using System.Globalization;
using MagicGradients;
using Xamarin.Forms;

namespace Playground.Converters
{
    public class OffsetToDoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((Offset)value).Value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Offset.Prop((double)value);
        }
    }
}
