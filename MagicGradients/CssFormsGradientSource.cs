using System.Collections.Generic;
using MagicGradients.Parser;
using Xamarin.Forms;

namespace MagicGradients
{
    [ContentProperty(nameof(Stylesheet))]
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
}
