namespace Playground.ViewModels
{
    public class LinearGradientsViewModel : BaseViewModel
    {
        private string _stylesheet;
        public string Stylesheet
        {
            get => _stylesheet;
            set => SetProperty(ref _stylesheet, value);
        }

        public LinearGradientsViewModel()
        {
            Stylesheet = "linear-gradient(red, yellow)";
        }
    }
}
