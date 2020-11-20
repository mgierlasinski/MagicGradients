using MagicGradients.Parser;
using Xamarin.Forms;

namespace MagicGradients
{
    [ContentProperty(nameof(Stylesheet))]
    public class CssGradientSource : GradientCollection
    {
        public static readonly BindableProperty StylesheetProperty = BindableProperty.Create(
            nameof(Stylesheet), typeof(string), typeof(CssGradientSource));

        public string Stylesheet
        {
            get => (string)GetValue(StylesheetProperty);
            set => SetValue(StylesheetProperty, value);
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            if (propertyName == nameof(Stylesheet))
            {
                var parsed = new CssGradientParser().ParseCss(Stylesheet);
                Gradients = new GradientElements<Gradient>(parsed);
            }

            base.OnPropertyChanged(propertyName);
        }
    }
}
