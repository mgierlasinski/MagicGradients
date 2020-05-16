using GradientParser.Commands;
using GradientParser.Models;
using GradientParser.Services;
using System.Windows.Input;
using Windows.ApplicationModel.DataTransfer;
using static System.String;
using static GradientParser.Models.Status;

namespace GradientParser.ViewModels
{
    public class MainViewModel: BindableBase
    {
        private readonly HtmlLoader _htmlLoader;
        private readonly HtmlParser _htmlParser;
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

        private string _tag;
        public string Tag
        {
            get => _tag;
            set =>SetProperty(ref _tag , value);
        }

        private Status  _status;
        public Status Status
        {
            get => _status;
            set =>SetProperty(ref _status , value);
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
            _htmlParser = new HtmlParser();
            _dialog = new Dialog();

            ParseGradientsCommand = new RunActionCommand(ParseGradients);
            CopyToClipboardCommand = new RunActionCommand(CopyToClipboard);

            _htmlLoader.HtmlLoaded += HtmlLoaded;
            Status = Waiting;
        }

        private void HtmlLoaded(object sender, string html)
        {
            Status = Parsing;
            Gradients = _htmlParser.Parse(html,Tag);
            Status = Waiting;
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

            Gradients = Empty;
            Status = Loading;
            _htmlLoader.StartLoading(Url);
        }
    }
}
