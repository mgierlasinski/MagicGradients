﻿using MagicGradients;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace Playground.Converters
{
    public class OffsetToRectConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new Rectangle(((Offset)value).Value, 0, 60, 1);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var bounds = (Rectangle)value;
            return bounds.X;
        }
    }
}
