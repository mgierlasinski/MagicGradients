using MagicGradients;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace Playground.Converters
{
    public class GradientNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is LinearGradient linear)
                return $"Linear ({linear.Stops.Count})";

            if (value is RadialGradient radial)
                return $"Radial ({radial.Stops.Count})";

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
