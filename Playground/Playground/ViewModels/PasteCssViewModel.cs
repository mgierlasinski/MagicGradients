using MagicGradients;
using MagicGradients.Parser;
using Playground.Data.Repositories;
using System;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Playground.ViewModels
{
    [QueryProperty("Id", "id")]
    public class PasteCssViewModel : BaseViewModel
    {
        private readonly IGradientRepository _gradientRepository;

        public ICommand RefreshCommand { get; set; }

        private string _cssCode;
        public string CssCode
        {
            get => _cssCode;
            set => SetProperty(ref _cssCode, value, onChanged: () =>
            {
                if (IsLiveRefresh)
                {
                    UpdateGradientSource();
                }
            });
        }

        private string _id;
        public string Id
        {
            get => _id;
            set
            {
                _id = value;
                LoadCssCodeById();
            }
        }

        private IGradientSource _gradientSource;
        public IGradientSource GradientSource
        {
            get => _gradientSource;
            set => SetProperty(ref _gradientSource, value, onChanged: ValidateEmptyData);
        }

        public bool IsMessageVisible => !string.IsNullOrWhiteSpace(Message);

        private string _message;
        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value, 
                onChanged: () => OnPropertyChanged(nameof(IsMessageVisible)));
        }

        private bool _isLiveRefresh = true;
        public bool IsLiveRefresh
        {
            get => _isLiveRefresh;
            set => SetProperty(ref _isLiveRefresh, value);
        }

        public PasteCssViewModel(IGradientRepository gradientRepository)
        {
            _gradientRepository = gradientRepository;

            RefreshCommand = new Command(UpdateGradientSource);

            UpdateGradientSource();
        }

        private void UpdateGradientSource()
        {
            Message = string.Empty;

            try
            {
                var parser = new CssGradientParser();
                var gradients = parser.ParseCss(CssCode);

                GradientSource = new GradientCollection
                {
                    Gradients = gradients.ToList()
                };
            }
            catch (Exception e)
            {
                Message = $"Invalid CSS: {e.Message}";
            }
        }

        private void LoadCssCodeById()
        {
            var gradient = _gradientRepository.GetById(new Guid(_id));

            if (gradient == null)
                return;

            _cssCode = gradient.Stylesheet;
            OnPropertyChanged(nameof(CssCode));
            UpdateGradientSource();
        }

        private void ValidateEmptyData()
        {
            if (GradientSource == null || !GradientSource.GetGradients().Any())
            {
                Message = "No gradient data";
            }
        }
    }
}
