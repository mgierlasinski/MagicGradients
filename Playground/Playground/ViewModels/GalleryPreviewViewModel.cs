using MagicGradients;
using Playground.Services;
using Xamarin.Forms;

namespace Playground.ViewModels
{
    [QueryProperty("Category", "category")]
    [QueryProperty("Id", "id")]
    public class GalleryPreviewViewModel : BaseViewModel
    {
        private readonly IGalleryService _galleryService;

        private ILinearGradientSource _gradient;
        public ILinearGradientSource Gradient
        {
            get => _gradient;
            set => SetProperty(ref _gradient, value);
        }

        public string Category { get; set; }

        private string _id;
        public string Id
        {
            get => _id;
            set
            {
                _id = value;
                Gradient = _galleryService.GetGradientById(Category,int.Parse(_id)).Source;
            }
        }

        public GalleryPreviewViewModel(IGalleryService galleryService)
        {
            _galleryService = galleryService;
        }
    }
}
