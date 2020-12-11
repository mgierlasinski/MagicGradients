using MagicGradients;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace Playground.Converters
{
    public class TabEnumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is BackgroundRepeat repeat)
                return (int)repeat;

            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //if (targetType.IsAssignableFrom(typeof(BackgroundRepeat)))
                return (BackgroundRepeat)value;
        }
    }
}
