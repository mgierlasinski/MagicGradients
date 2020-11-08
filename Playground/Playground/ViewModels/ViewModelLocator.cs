using Playground.Data.Repositories;
using Playground.Features.BattleTest;
using Playground.Features.BattleTest.Services;
using Playground.Features.CssPreviewer;
using Playground.Features.Gallery;
using Playground.Features.Gallery.Services;
using Playground.Features.LinearGradient;
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

        public CssPreviewerViewModel CssPreviewerViewModel => new CssPreviewerViewModel(
            DependencyService.Get<IGradientRepository>());

        public BattleTestViewModel BattleTestViewModel => new BattleTestViewModel(
            DependencyService.Get<IGradientRepository>(),
            DependencyService.Get<IPickerColorsDataProvider>(),
            DependencyService.Get<IBattleItemService>());

        public LinearGradientsViewModel LinearGradientsViewModel => new LinearGradientsViewModel();

        public LinearGradientSamplesViewModel LinearGradientSamplesViewModel => new LinearGradientSamplesViewModel();
    }
}
