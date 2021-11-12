using GradientsApp.ViewModels;
using Playground.Data.Infrastructure;
using Playground.Data.Repositories;
using SimpleInjector;

namespace GradientsApp
{
    public static class AppSetup
    {
        public static Container IoC { get; } = new Container();

        public static void ConfigureAndRun()
        {
            RegisterTypes();
            RegisterViewModels();
            InitializeDatabase();
        }

        public static void RegisterTypes()
        {
            IoC.Register<IDatabaseProvider, DatabaseProvider>();
            IoC.Register<IDatabaseUpdater, DatabaseUpdater>();

            IoC.Register<IDocumentRepository, DocumentRepository>();
            IoC.Register<IGradientRepository, GradientRepository>();
            IoC.Register<ICategoryRepository, CategoryRepository>();
        }

        public static void RegisterViewModels()
        {
            IoC.Register<HomeViewModel>();
            IoC.Register<LinearViewModel>();
        }

        public static void InitializeDatabase()
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
