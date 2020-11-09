using MagicGradients;
using MagicGradients.Parser;
using MagicGradients.Xaml;
using Playground.Data.Repositories;
using Playground.ViewModels;
using System;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Playground.Features.CssPreviewer
{
    [QueryProperty("Id", "id")]
    public class CssPreviewerViewModel : BaseViewModel
    {
        private readonly IGradientRepository _gradientRepository;
        private readonly DimensionsTypeConverter _dimensionsConverter;

        public ICommand RefreshCommand { get; set; }

        private string _cssCode;
        public string CssCode
        {
            get => _cssCode;
            set => SetProperty(ref _cssCode, value, () =>
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
            set => SetProperty(ref _size, value, () =>
            {
                if (IsHotReload)
                {
                    UpdateSize();
                }
            });
        }

        public Dimensions GradientSize { get; private set; }

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

        private GradientCollection _gradientSource;
        public GradientCollection GradientSource
        {
            get => _gradientSource;
            set => SetProperty(ref _gradientSource, value, ValidateEmptyData);
        }

        public bool IsMessageVisible => !string.IsNullOrWhiteSpace(Message);

        private string _message;
        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value, 
                () => OnPropertyChanged(nameof(IsMessageVisible)));
        }

        private bool _isHotReload = true;
        public bool IsHotReload
        {
            get => _isHotReload;
            set => SetProperty(ref _isHotReload, value);
        }

        public CssPreviewerViewModel(IGradientRepository gradientRepository)
        {
            _gradientRepository = gradientRepository;
            _dimensionsConverter = new DimensionsTypeConverter();

            GradientSource = new GradientCollection();
            RefreshCommand = new Command(() =>
            {
                UpdateGradientSource();
                UpdateSize();
            });
        }

        private void UpdateGradientSource()
        {
            Message = string.Empty;

            try
            {
                var parser = new CssGradientParser();
                var gradients = parser.ParseCss(CssCode);

                GradientSource.Gradients = new GradientElements<Gradient>(gradients);
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
                OnPropertyChanged(nameof(GradientSize));
            }
            catch (Exception e)
            {
                Message = $"Invalid size: {e.Message}";
            }
        }

        private void LoadCssCodeById()
        {
            var gradient = _gradientRepository.GetById(int.Parse(_id));

            if (gradient == null)
                return;

            CssCode = gradient.Stylesheet;
            Size = gradient.Size;
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
