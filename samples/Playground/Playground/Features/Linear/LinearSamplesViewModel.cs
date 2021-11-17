using Playground.ViewModels;

namespace Playground.Features.Linear
{
    public class LinearSamplesViewModel : ObservableObject
    {
        private string _stylesheet;
        public string Stylesheet
        {
            get => _stylesheet;
            set => SetProperty(ref _stylesheet, value);
        }

        public LinearSamplesViewModel()
        {
            Stylesheet = "linear-gradient(red, yellow)";
        }
    }
}
