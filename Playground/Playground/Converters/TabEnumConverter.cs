using System;
using System.Globalization;
using Xamarin.Forms;

namespace Playground.Converters
{
    public class TabEnumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Enum.ToObject(targetType, (int)value);
        }
    }
}
