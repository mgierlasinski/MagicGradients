using MagicGradients;
using Playground.Services;
using System;
using Xamarin.Forms;

namespace Playground.ViewModels
{
    [QueryProperty("Category", "category")]
    [QueryProperty("Id", "id")]
    public class GalleryPreviewViewModel : BaseViewModel
    {
        private readonly IGalleryService _galleryService;

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
        }
    }
}
