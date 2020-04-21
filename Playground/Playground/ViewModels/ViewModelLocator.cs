using Playground.Data.Repositories;
using Playground.Services;
using Xamarin.Forms;

namespace Playground.ViewModels
{
    public class ViewModelLocator
    {
        public GalleryCategoriesViewModel GalleryCategoriesViewModel => new GalleryCategoriesViewModel(
            DependencyService.Get<ICategoryService>());

        public GalleryListViewModel GalleryListViewModel => new GalleryListViewModel(
            DependencyService.Get<IGalleryService>(),
            DependencyService.Get<ICategoryService>());

        public GalleryPreviewViewModel GalleryPreviewViewModel => new GalleryPreviewViewModel(
            DependencyService.Get<IGalleryService>());

        public PasteCssViewModel PasteCssViewModel => new PasteCssViewModel(
            DependencyService.Get<IGradientRepository>());

        public BattleTestViewModel BattleTestViewModel => new BattleTestViewModel(
            DependencyService.Get<IGradientRepository>(),
            DependencyService.Get<IPickerColorsDataProvider>(),
            DependencyService.Get<IBattleItemService>());

        public LinearGradientsViewModel LinearGradientsViewModel => new LinearGradientsViewModel();
    }
}
