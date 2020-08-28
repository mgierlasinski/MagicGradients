﻿using MagicGradients;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace PlaygroundLite.Converters
{
    public class OffsetToRectConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new Rectangle(((Offset)value).Value, 0, 20, 1);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var bounds = (Rectangle)value;
            return Offset.Prop(bounds.X);
        }
    }
}
