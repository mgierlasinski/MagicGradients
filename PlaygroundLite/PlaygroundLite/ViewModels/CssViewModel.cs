using MagicGradients;
using MagicGradients.Parser;
using PlaygroundLite.Models;
using PlaygroundLite.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MagicGradients.Xaml;
using Xamarin.Forms;

namespace PlaygroundLite.ViewModels
{
    public class CssViewModel : BaseViewModel
    {
        private CssSnippet[] _snippets;
        private readonly DimensionsTypeConverter _dimensionsConverter;

        private string _cssCode;
        public string CssCode
        {
            get => _cssCode;
            set => SetProperty(ref _cssCode, value, onChanged: () =>
            {
                if (IsHotReload)
                {
                    UpdateGradientSource();
                }
            });
        }

        private string _size;
        public string Size
        {
            get => _size;
            set => SetProperty(ref _size, value, onChanged: () =>
            {
                if (IsHotReload)
                {
                    UpdateSize();
                }
            });
        }

        public Dimensions GradientSize { get; private set; }
        public GradientCollection GradientSource { get; set; }
        
        public bool IsMessageVisible => !string.IsNullOrWhiteSpace(Message);

        private string _message;
        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value,
                onChanged: () => RaisePropertyChanged(nameof(IsMessageVisible)));
        }

        private bool _isHotReload = true;
        public bool IsHotReload
        {
            get => _isHotReload;
            set => SetProperty(ref _isHotReload, value);
        }

        public ICommand ClearCommand { get; }
        public ICommand ShowSnippetsCommand { get; }
        public ICommand RefreshCommand { get; }

        public CssViewModel()
        {
            _dimensionsConverter = new DimensionsTypeConverter();
            GradientSource = new GradientCollection();

            ClearCommand = new Command(() => CssCode = string.Empty);
            ShowSnippetsCommand = new Command(() => ShowSnippetsActionSheet());
            RefreshCommand = new Command(() =>
            {
                UpdateGradientSource();
                UpdateSize();
            });

            LoadSnippets();
            UpdateGradientSource();
        }

        private void LoadSnippets()
        {
            var provider = new SnippetProvider();
            _snippets = provider.GetCssSnippets();
        }

        private async Task ShowSnippetsActionSheet()
        {
            var values = _snippets.Select(x => x.Name).ToArray();
            var result = await CoreMethods.DisplayActionSheet("Pick gradient", "Cancel", null, values);
            var selection = _snippets.FirstOrDefault(x => x.Name == result);

            if (selection != null)
            {
                if (string.IsNullOrEmpty(CssCode))
                {
                    CssCode = selection.Code;
                    return;
                }

                CssCode = CssCode.EndsWith(",") || CssCode.EndsWith(", ") ? 
                    $"{CssCode}{selection.Code}" : 
                    $"{CssCode},{selection.Code}";
            }
        }

        private void UpdateGradientSource()
        {
            Message = string.Empty;

            try
            {
                var parser = new CssGradientParser();
                var gradients = parser.ParseCss(CssCode);

                GradientSource.Gradients = new GradientElements<Gradient>(gradients);
                ValidateEmptyData();
            }
            catch (Exception e)
            {
                Message = $"Invalid CSS: {e.Message}";
            }
        }

        private void UpdateSize()
        {
            Message = string.Empty;

            try
            {
                var size = (Dimensions)_dimensionsConverter.ConvertFromInvariantString(Size);
                GradientSize = size;
                RaisePropertyChanged(nameof(GradientSize));
            }
            catch (Exception e)
            {
                Message = $"Invalid size: {e.Message}";
            }
        }

        private void ValidateEmptyData()
        {
            if (!GradientSource.Gradients.Any())
            {
                Message = "No gradient data";
            }
        }
    }
}
