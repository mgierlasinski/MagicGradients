using GradientsApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Playground.Data.Infrastructure;
using Playground.Data.Repositories;

namespace GradientsApp
{
    public class AppSetupNew
    {
        public void Setup(IServiceCollection services)
        {
            Configure();
            RegisterTypes(services);
            RegisterViewModels(services);
            ConfigureServices(services);
        }

        private void RegisterTypes(IServiceCollection services)
        {
            services.AddSingleton<IDatabaseProvider, DatabaseProvider>();
            services.AddSingleton<IDatabaseUpdater, DatabaseUpdater>();
            services.AddSingleton<IDocumentRepository, DocumentRepository>();
            services.AddSingleton<IGradientRepository, GradientRepository>();
            services.AddSingleton<ICategoryRepository, CategoryRepository>();

            services.AddSingleton<AppStartup>();
        }

        private void RegisterViewModels(IServiceCollection services)
        {
            services.AddTransient<HomeViewModel>();
            services.AddTransient<LinearViewModel>();
            services.AddTransient<CategoriesViewModel>();
            services.AddTransient<GalleryViewModel>();
            services.AddTransient<GradientViewModel>();
        }

        protected virtual void Configure()
        {

        }

        protected virtual void ConfigureServices(IServiceCollection services)
        {

        }
    }
}
