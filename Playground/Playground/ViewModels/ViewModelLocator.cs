using Playground.Services;
using Xamarin.Forms;

namespace Playground.ViewModels
{
    public class ViewModelLocator
    {
        public GalleryListViewModel GalleryListViewModel => new GalleryListViewModel(DependencyService.Get<IGalleryService>());

        public GalleryPreviewViewModel GalleryPreviewViewModel => new GalleryPreviewViewModel(DependencyService.Get<IGalleryService>());
    }
}
