using GradientParser.Commands;
using GradientParser.Services;
using System.Windows.Input;
using Windows.ApplicationModel.DataTransfer;
using static System.String;

namespace GradientParser.ViewModels
{
    public class MainViewModel: BindableBase
    {
        private readonly HtmlLoader _htmlLoader;
        private readonly Dialog _dialog;

        private string _gradients;
        public string Gradients
        {
            get => _gradients;
            set
            {
                if (SetProperty(ref _gradients, value))
                    RaisePropertyChanged(nameof(IsGradientsExist));
            }
        }

        private string _url;
        public string Url
        {
            get => _url;
            set => SetProperty(ref _url, value);
        }

        public bool IsGradientsExist => !IsNullOrWhiteSpace(Gradients);

        public ICommand ParseGradientsCommand { get; }
        public ICommand CopyToClipboardCommand { get; }

        public MainViewModel()
        {
            _htmlLoader = new HtmlLoader();
            _dialog = new Dialog();

            ParseGradientsCommand = new RunActionCommand(ParseGradients);
            CopyToClipboardCommand = new RunActionCommand(CopyToClipboard);

            _htmlLoader.HtmlLoaded += HtmlLoaded;
        }

        private void HtmlLoaded(object sender, string e)
        {
            Gradients = e;
        }

        private void CopyToClipboard()
        {
            var dataPackage = new DataPackage();
            dataPackage.SetText(Gradients);
            Clipboard.SetContent(dataPackage);
        }

        private async void ParseGradients()
        {
            if (IsNullOrWhiteSpace(Url))
            {
                await _dialog.ShowAlert("Please Enter URL");
                return;
            }


            _htmlLoader.StartLoading(Url);
            //todo Load Web Page
            //todo Parse Content
            //todo Add each Gradient into Gradients Property
        }
    }
}
