using MagicGradients;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace Playground.Converters
{
    public class OffsetToPercentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Math.Floor(((Offset)value).Value * 100);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (double.TryParse((string) value, out var parsed))
            {
                return Offset.Prop(parsed / 100);
            }
            return Offset.Zero;
        }
    }
}
