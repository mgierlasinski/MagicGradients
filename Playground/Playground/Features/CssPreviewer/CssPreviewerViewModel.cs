using MagicGradients;
using MagicGradients.Parser;
using MagicGradients.Xaml;
using Playground.Data.Repositories;
using System;
using System.Collections.Generic;
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
        private readonly Dictionary<string, string> _errors = new Dictionary<string, string>();
        private readonly CssSnippet[] _snippets;

        public CssGradientSource GradientSource { get; set; }

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

        private bool _isHotReload = true;
        public bool IsHotReload
        {
            get => _isHotReload;
            set => SetProperty(ref _isHotReload, value);
        }

        public string Message => string.Join(Environment.NewLine, _errors.Values);
        public bool IsMessageVisible => !string.IsNullOrWhiteSpace(Message);

        public ICommand ClearCommand { get; }
        public ICommand ShowSnippetsCommand { get; }
        public ICommand RefreshCommand { get; }

        public CssPreviewerViewModel(IGradientRepository gradientRepository) 
            : base(gradientRepository)
        {
            _dimensionsConverter = new DimensionsTypeConverter();
            _repeatConverter = new BackgroundRepeatTypeConverter();
            _snippets = new CssSnippetProvider().GetCssSnippets();

            GradientSource = new CssGradientSource();

            ClearCommand = new Command(() => CssCode = string.Empty);
            ShowSnippetsCommand = new Command(() => ShowSnippetsActionSheet());
            RefreshCommand = new Command(() =>
            {
                UpdateGradientSource();
                UpdateGradientSize();
                UpdateGradientRepeat();
            });

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
            try
            {
                var parser = new CssGradientParser();
                var gradients = parser.ParseCss(CssCode);
                GradientSource.Gradients = new GradientElements<Gradient>(gradients);

                if (!GradientSource.GetGradients().Any())
                {
                    SetError(nameof(CssCode), "No gradient data");
                    return;
                } 
                
                RemoveError(nameof(CssCode));
            }
            catch (Exception e)
            {
                SetError(nameof(CssCode), e.Message);
            }
        }

        private void UpdateGradientSize()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(CssSize))
                    GradientSize = (Dimensions)_dimensionsConverter.ConvertFromInvariantString(CssSize);

                RemoveError(nameof(CssSize));
            }
            catch (Exception e)
            {
                SetError(nameof(CssSize), e.Message);
            }
        }

        private void UpdateGradientRepeat()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(CssRepeat))
                    GradientRepeat = (BackgroundRepeat)_repeatConverter.ConvertFromInvariantString(CssRepeat);

                RemoveError(nameof(CssRepeat));
            }
            catch (Exception e)
            {
                SetError(nameof(CssRepeat), e.Message);
            }
        }

        private void SetError(string key, string error)
        {
            if (_errors.ContainsKey(key))
                _errors[key] = error;
            else
                _errors.Add(key, error);

            RaisePropertyChanged(nameof(Message));
            RaisePropertyChanged(nameof(IsMessageVisible));
        }

        private void RemoveError(string key)
        {
            if (_errors.ContainsKey(key))
                _errors.Remove(key);

            RaisePropertyChanged(nameof(Message));
            RaisePropertyChanged(nameof(IsMessageVisible));
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
