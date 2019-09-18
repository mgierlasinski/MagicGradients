using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MagicGradients;
using MagicGradients.Parser;
using Xamarin.Forms;
using Xamarin.Forms.StyleSheets;
using Xamarin.Forms.Xaml;

namespace MagicGradients
{
    [ContentProperty(nameof(Stylesheet))]
    [TypeConverter(typeof(CssFormsGradientTypeConverter))]
    public class CssFormsGradientSource : BindableObject, ILinearGradientSource
    {
        public static readonly BindableProperty StylesheetProperty = BindableProperty.Create(
            nameof(Stylesheet), typeof(string), typeof(CssFormsGradientSource));

        public string Stylesheet
        {
            get => (string)GetValue(StylesheetProperty);
            set => SetValue(StylesheetProperty, value);
        }

        public IEnumerable<LinearGradient> GetGradients()
        {
            return new CssFormsLinearGradientParser().ParseCss(Stylesheet);
        }
    }

    [TypeConversion(typeof(CssFormsGradientSource))]
    public class CssFormsGradientTypeConverter : TypeConverter
    {
        public override object ConvertFromInvariantString(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new InvalidOperationException($"Cannot convert \"{value}\" into {typeof(CssFormsGradientSource)}");

            return new CssFormsGradientSource
            {
                Stylesheet = value
            };
        }
    }
}
