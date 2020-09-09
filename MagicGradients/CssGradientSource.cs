using MagicGradients.Parser;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MagicGradients
{
    [ContentProperty(nameof(Stylesheet))]
    public class CssGradientSource : GradientElement, IGradientSource
    {
        private Gradient[] _internalGradients = new Gradient[0];

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
                _internalGradients = new CssGradientParser().ParseCss(Stylesheet);
            }

            base.OnPropertyChanged(propertyName);
        }

        public IEnumerable<Gradient> GetGradients() => _internalGradients;
    }
}
