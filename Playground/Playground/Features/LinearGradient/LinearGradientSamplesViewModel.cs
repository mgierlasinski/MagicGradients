using Playground.ViewModels;

namespace Playground.Features.LinearGradient
{
    public class LinearGradientSamplesViewModel : BaseViewModel
    {
        private string _stylesheet;
        public string Stylesheet
        {
            get => _stylesheet;
            set => SetProperty(ref _stylesheet, value);
        }

        public LinearGradientSamplesViewModel()
        {
            Stylesheet = "linear-gradient(red, yellow)";
        }
    }
}
