using MagicGradients.Parser;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MagicGradients
{
    [ContentProperty(nameof(Stylesheet))]
    public class CssNativeGradientSource : BindableObject, ILinearGradientSource
    {
        public static readonly BindableProperty StylesheetProperty = BindableProperty.Create(
            nameof(Stylesheet), typeof(string), typeof(CssNativeGradientSource));

        public string Stylesheet
        {
            get => (string)GetValue(StylesheetProperty);
            set => SetValue(StylesheetProperty, value);
        }

        public IEnumerable<LinearGradient> GetGradients()
        {
            return new CssNativeLinearGradientParser().ParseCss(Stylesheet);
        }
    }
}
