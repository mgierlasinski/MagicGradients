using Playground.Data.Repositories;
using Playground.ViewModels;
using System;
using Xamarin.Forms;

namespace Playground.Features.CssPreviewer
{
    [QueryProperty("Id", "id")]
    [QueryProperty("Data", "data")]
    public class CssPreviewerBase : ObservableObject
    {
        private readonly IGradientRepository _gradientRepository;

        private string _id;
        public string Id
        {
            get => _id;
            set
            {
                _id = value;
                LoadCssCodeFromGallery(int.Parse(_id));
            }
        }

        private string _data;
        public string Data
        {
            get => _data;
            set
            {
                _data = value;
                ParseData(_data);
            }
        }

        private string _cssCode;
        public string CssCode
        {
            get => _cssCode;
            set => SetProperty(ref _cssCode, value);
        }

        private string _cssSize;
        public string CssSize
        {
            get => _cssSize;
            set => SetProperty(ref _cssSize, value);
        }

        private string _cssRepeat;
        public string CssRepeat
        {
            get => _cssRepeat;
            set => SetProperty(ref _cssRepeat, value);
        }

        public CssPreviewerBase(IGradientRepository gradientRepository)
        {
            _gradientRepository = gradientRepository;
        }

        private void LoadCssCodeFromGallery(int id)
        {
            var gradient = _gradientRepository.GetById(id);

            if (gradient == null)
                return;

            CssCode = gradient.Stylesheet;
            CssSize = gradient.Size;
        }

        private void ParseData(string data)
        {
            var parts = Uri.UnescapeDataString(data).Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length >= 1)
                CssCode = parts[0];

            if (parts.Length >= 2)
                CssSize = parts[1];

            if (parts.Length >= 3)
                CssRepeat = parts[2];
        }
    }
}
