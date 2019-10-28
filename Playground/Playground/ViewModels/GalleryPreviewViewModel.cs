using MagicGradients;
using Playground.Services;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Playground.ViewModels
{
    [QueryProperty("Id", "id")]
    public class GalleryPreviewViewModel : BaseViewModel
    {
        private readonly IGalleryService _galleryService;

        public ICommand PreviewCssCommand { get; set; }

        private IGradientSource _gradient;
        public IGradientSource Gradient
        {
            get => _gradient;
            set => SetProperty(ref _gradient, value);
        }

        private string _id;
        public string Id
        {
            get => _id;
            set
            {
                _id = value;
                Gradient = _galleryService.GetGradientById(new Guid(_id)).Source;
            }
        }

        public GalleryPreviewViewModel(IGalleryService galleryService)
        {
            _galleryService = galleryService;

            PreviewCssCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync($"PasteCss?id={Id}");
            });
        }
    }
}
