using MagicGradients;
using Playground.Resources.Fonts;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace Playground.Converters
{
    public class GradientIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var icon = new FontImageSource
            {
                FontFamily = "IcoMoon"
            };

            if (value is LinearGradient)
                icon.Glyph = IcoMoon.Gradient;

            if (value is RadialGradient)
                icon.Glyph = IcoMoon.Radial;

            return icon;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
