using MagicGradients;
using MagicGradients.Parser;
using MagicGradients.Xaml;
using Playground.Data.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Playground.Features.CssPreviewer
{
    public class CssPreviewerViewModel : CssPreviewerBase
    {
        private readonly DimensionsTypeConverter _dimensionsConverter;
        private readonly BackgroundRepeatTypeConverter _repeatConverter;
        private CssSnippet[] _snippets;
        
        public GradientCollection GradientSource { get; set; }

        private Dimensions _gradientSize;
        public Dimensions GradientSize
        {
            get => _gradientSize;
            private set => SetProperty(ref _gradientSize, value);
        }

        private BackgroundRepeat _gradientRepeat;
        public BackgroundRepeat GradientRepeat
        {
            get => _gradientRepeat;
            private set => SetProperty(ref _gradientRepeat, value);
        }

        public bool IsMessageVisible => !string.IsNullOrWhiteSpace(Message);

        private string _message;
        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value, 
                () => RaisePropertyChanged(nameof(IsMessageVisible)));
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

        public CssPreviewerViewModel(IGradientRepository gradientRepository) 
            : base(gradientRepository)
        {
            _dimensionsConverter = new DimensionsTypeConverter();
            _repeatConverter = new BackgroundRepeatTypeConverter();

            GradientSource = new GradientCollection();

            ClearCommand = new Command(() => CssCode = string.Empty);
            ShowSnippetsCommand = new Command(() => ShowSnippetsActionSheet());
            RefreshCommand = new Command(() =>
            {
                UpdateGradientSource();
                UpdateGradientSize();
                UpdateGradientRepeat();
            });

            LoadSnippets();
            UpdateGradientSource();
        }

        protected override void OnPropertyChanged(string propertyName)
        {
            if (!IsHotReload)
                return;

            if(propertyName == nameof(CssCode))
                UpdateGradientSource();

            if (propertyName == nameof(CssSize))
                UpdateGradientSize();

            if (propertyName == nameof(CssRepeat))
                UpdateGradientRepeat();
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

        private void UpdateGradientSize()
        {
            Message = string.Empty;

            try
            {
                GradientSize = (Dimensions)_dimensionsConverter.ConvertFromInvariantString(CssSize);
            }
            catch (Exception e)
            {
                Message = $"Invalid size: {e.Message}";
            }
        }

        private void UpdateGradientRepeat()
        {
            Message = string.Empty;

            try
            {
                GradientRepeat = (BackgroundRepeat)_repeatConverter.ConvertFromInvariantString(CssRepeat);
            }
            catch (Exception e)
            {
                Message = $"Invalid repeat: {e.Message}";
            }
        }

        private void ValidateEmptyData()
        {
            if (!GradientSource.GetGradients().Any())
            {
                Message = "No gradient data";
            }
        }

        private void LoadSnippets()
        {
            var provider = new CssSnippetProvider();
            _snippets = provider.GetCssSnippets();
        }

        private async Task ShowSnippetsActionSheet()
        {
            var values = _snippets.Select(x => x.Name).ToArray();
            var result = await Shell.Current.DisplayActionSheet("Pick gradient", "Cancel", null, values);
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
    }
}
