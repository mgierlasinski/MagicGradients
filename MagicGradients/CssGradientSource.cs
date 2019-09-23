using System;
using MagicGradients.Parser;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MagicGradients
{
    [ContentProperty(nameof(Stylesheet))]
    public class CssGradientSource : BindableObject, ILinearGradientSource
    {
        public static readonly BindableProperty StylesheetProperty = BindableProperty.Create(
            nameof(Stylesheet), typeof(string), typeof(CssGradientSource));

        public string Stylesheet
        {
            get => (string)GetValue(StylesheetProperty);
            set => SetValue(StylesheetProperty, value);
        }

        public IEnumerable<LinearGradient> GetGradients()
        {
            return new CssLinearGradientParser().ParseCss(Stylesheet);
        }
    }

    [TypeConversion(typeof(ILinearGradientSource))]
    public class CssGradientSourceTypeConverter : TypeConverter
    {
        public override object ConvertFromInvariantString(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new InvalidOperationException($"Cannot convert \"{value}\" into {typeof(CssGradientSource)}");

            return new CssGradientSource
            {
                Stylesheet = value
            };
        }
    }
}
