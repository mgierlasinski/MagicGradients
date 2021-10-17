using MagicGradients.Parser;
using Xamarin.Forms;

namespace MagicGradients
{
    [ContentProperty(nameof(Stylesheet))]
    public class CssGradientSource : GradientCollection
    {
        private readonly CssGradientParser _parser = new CssGradientParser(GlobalSetup.Current.GradientFactory);

        public static readonly BindableProperty StylesheetProperty = BindableProperty.Create(
            nameof(Stylesheet), typeof(string), typeof(CssGradientSource), propertyChanged: OnStylesheetChanged);

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
            var parsed = _parser.ParseAs<Gradient>(css);
            Gradients = new GradientElements<Gradient>(parsed);
        }
    }
}
