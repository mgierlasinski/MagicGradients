using Playground.Data.Infrastructure;
using Playground.Data.Repositories;
using Playground.Services;
using Xamarin.Forms;

namespace Playground
{
    public static class IoC
    {
        public static void RegisterTypes()
        {
            DependencyService.Register<DatabaseProvider>();
            DependencyService.Register<DatabaseUpdater>();

            DependencyService.Register<DocumentRepository>();
            DependencyService.Register<GradientRepository>();
            DependencyService.Register<CategoryRepository>();

            DependencyService.Register<GalleryService>();
            DependencyService.Register<CategoryService>();
            DependencyService.Register<BattleItemService>();

            DependencyService.Register<IPickerColorsDataProvider, PickerColorsDataProvider>();
        }

        public static void Initialize()
        {
            DependencyService.Resolve<IDocumentRepository>().SetupMapper();

            DependencyService.Resolve<IDatabaseUpdater>().RunUpdate(
                DependencyService.Resolve<IGradientRepository>(),
                DependencyService.Resolve<ICategoryRepository>());
        }
    }
}
