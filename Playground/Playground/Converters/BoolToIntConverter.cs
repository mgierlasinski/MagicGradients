using System;
using System.Globalization;
using Xamarin.Forms;

namespace Playground.Converters
{
    public class BoolToIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool bValue)
                return bValue ? 1 : 0;

            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int iValue)
                return iValue == 1;

            return false;
        }
    }
}
