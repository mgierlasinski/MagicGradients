using MagicGradients.Parser;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MagicGradients
{
    [ContentProperty(nameof(Stylesheet))]
    public class CssGradientSource : GradientElement, IGradientSource
    {
        private readonly CssGradientParserSource _parserSource = new();

        public static readonly BindableProperty StylesheetProperty = BindableProperty.Create(
            nameof(Stylesheet), 
            typeof(string), 
            typeof(CssGradientSource), 
            propertyChanged: OnStylesheetChanged);

        public string Stylesheet
        {
            get => (string)GetValue(StylesheetProperty);
            set => SetValue(StylesheetProperty, value);
        }

        private static void OnStylesheetChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((CssGradientSource)bindable).InternalParse((string)newValue);
        }

        private void InternalParse(string css)
        {
            _parserSource.Parse(css);
        }

        public IReadOnlyList<IGradient> GetGradients()
        {
            return _parserSource.GetGradients();
        }
    }
}
