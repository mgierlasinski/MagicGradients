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
            var icoMoon = new IcoMoon();

            if (value is LinearGradient)
                icoMoon.Glyph = IcoMoon.Gradient;

            if (value is RadialGradient)
                icoMoon.Glyph = IcoMoon.Radial;

            return icoMoon.GetIcon();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
