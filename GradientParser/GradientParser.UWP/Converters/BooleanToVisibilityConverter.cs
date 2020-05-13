using System;
using Windows.UI.Xaml.Data;
using static Windows.UI.Xaml.Visibility;

namespace GradientParser.Converters
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (bool.TryParse(value?.ToString(), out var isVisible))
            {
                return isVisible ? Visible : Collapsed;
            }

            return Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
