using Playground.Data.Repositories;
using Playground.Services;
using Xamarin.Forms;

namespace Playground.ViewModels
{
    public class ViewModelLocator
    {
        public GalleryCategoriesViewModel GalleryCategoriesViewModel => new GalleryCategoriesViewModel(DependencyService.Get<IGalleryService>());

        public GalleryListViewModel GalleryListViewModel => new GalleryListViewModel(DependencyService.Get<IGalleryService>());

        public GalleryPreviewViewModel GalleryPreviewViewModel => new GalleryPreviewViewModel(DependencyService.Get<IGalleryService>());

        public PasteCssViewModel PasteCssViewModel => new PasteCssViewModel(DependencyService.Get<IGradientRepository>());
    }
}
