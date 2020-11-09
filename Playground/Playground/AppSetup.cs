using Playground.Data.Infrastructure;
using Playground.Data.Repositories;
using Playground.Features.Animation;
using Playground.Features.BattleTest;
using Playground.Features.CssPreviewer;
using Playground.Features.Gallery;
using Playground.Features.Gallery.Services;
using Playground.Features.Home;
using Playground.Features.Linear;
using Playground.Features.Radial;
using SimpleInjector;
using Xamarin.Forms;

namespace Playground
{
    public static class AppSetup
    {
        public static Container IoC { get; } = new Container();

        public static void RegisterTypes()
        {
            IoC.Register<IDatabaseProvider, DatabaseProvider>();
            IoC.Register<IDatabaseUpdater, DatabaseUpdater>();

            IoC.Register<IDocumentRepository, DocumentRepository>();
            IoC.Register<IGradientRepository, GradientRepository>();
            IoC.Register<ICategoryRepository, CategoryRepository>();

            IoC.Register<IGalleryService, GalleryService>();
            IoC.Register<ICategoryService, CategoryService>();
            IoC.Register<IBattleItemService, BattleItemService>();
        }

        public static void RegisterViewModels()
        {
            IoC.Register<HomeViewModel>();
            IoC.Register<LinearGradientViewModel>();
            IoC.Register<LinearSamplesViewModel>();
            IoC.Register<RadialGradientViewModel>();
            IoC.Register<GalleryCategoriesViewModel>();
            IoC.Register<GalleryListViewModel>();
            IoC.Register<GalleryPreviewViewModel>();
            IoC.Register<CssPreviewerViewModel>();
            IoC.Register<BattleTestViewModel>();
            IoC.Register<AnimationsViewModel>();
        }

        public static void RegisterRoutes()
        {
            Routing.RegisterRoute("LinearGradient", typeof(LinearGradientPage));
            Routing.RegisterRoute("RadialGradient", typeof(RadialGradientPage));
            Routing.RegisterRoute("GalleryList", typeof(GalleryListPage));
            Routing.RegisterRoute("GalleryPreview", typeof(GalleryPreviewPage));
            Routing.RegisterRoute("CssPreviewer", typeof(CssPreviewerPage));
            Routing.RegisterRoute("BattleTest", typeof(BattleTestPage));
        }

        public static void Initialize()
        {
            IoC.GetInstance<IDocumentRepository>().SetupMapper();

            var repositories = new ICanUpdateMyself[]
            {
                IoC.GetInstance<IGradientRepository>(),
                IoC.GetInstance<ICategoryRepository>()
            };

            IoC.GetInstance<IDatabaseUpdater>().RunUpdate(repositories);
        }
    }
}
