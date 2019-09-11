using MagicGradients;
using Playground.Services;
using Xamarin.Forms;

namespace Playground.ViewModels
{
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

        private string _id;
        public string Id
        {
            get => _id;
            set
            {
                _id = value;
                Gradient = _galleryService.GetGradientById(int.Parse(_id)).Source;
            }
        }

        public GalleryPreviewViewModel()
        {
            _galleryService = DependencyService.Get<IGalleryService>();
        }
    }
}
