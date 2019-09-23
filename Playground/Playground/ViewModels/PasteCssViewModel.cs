using MagicGradients;
using MagicGradients.Parser;
using System;
using System.Linq;

namespace Playground.ViewModels
{
    public class PasteCssViewModel : BaseViewModel
    {
        private string _cssCode;
        public string CssCode
        {
            get => _cssCode;
            set => SetProperty(ref _cssCode, value, onChanged: UpdateGradientSource);
        }

        private ILinearGradientSource _gradientSource;
        public ILinearGradientSource GradientSource
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

        public PasteCssViewModel()
        {
            UpdateGradientSource();
        }

        private void UpdateGradientSource()
        {
            Message = string.Empty;

            try
            {
                var parser = new CssLinearGradientParser();
                var gradients = parser.ParseCss(CssCode);

                GradientSource = new LinearGradientSource
                {
                    Gradients = gradients.ToList()
                };
            }
            catch (Exception e)
            {
                Message = $"Invalid CSS: {e.Message}";
            }
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
