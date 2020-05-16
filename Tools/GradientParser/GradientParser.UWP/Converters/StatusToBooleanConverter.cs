using GradientParser.Models;
using System;
using Windows.UI.Xaml.Data;

namespace GradientParser.Converters
{
    public class StatusToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (Enum.TryParse(typeof(Status), value?.ToString(), out var status))
            {
                return (Status)status != Status.Waiting;
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
