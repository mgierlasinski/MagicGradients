using GradientsApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Playground.Data.Infrastructure;
using Playground.Data.Repositories;
using System;

namespace GradientsApp
{
    public class AppSetup
    {
        public void ConfigureAndRun()
        {
            Configure();

            var services = new ServiceCollection();

            RegisterTypes(services);
            RegisterViewModels(services);
            ConfigureServices(services);

            Ioc.Default = services.BuildServiceProvider();

            InitializeDatabase();
        }

        private void RegisterTypes(IServiceCollection services)
        {
            services.AddSingleton<IDatabaseProvider, DatabaseProvider>();
            services.AddSingleton<IDatabaseUpdater, DatabaseUpdater>();
            services.AddSingleton<IDocumentRepository, DocumentRepository>();
            services.AddSingleton<IGradientRepository, GradientRepository>();
            services.AddSingleton<ICategoryRepository, CategoryRepository>();
        }

        private void RegisterViewModels(IServiceCollection services)
        {
            services.AddTransient<HomeViewModel>();
            services.AddTransient<LinearViewModel>();
            services.AddTransient<CategoriesViewModel>();
        }

        private void InitializeDatabase()
        {
            Ioc.Default.GetService<IDocumentRepository>()?.SetupMapper();

            var repositories = new ICanUpdateMyself[]
            {
                Ioc.Default.GetService<IGradientRepository>(),
                Ioc.Default.GetService<ICategoryRepository>()
            };

            Ioc.Default.GetService<IDatabaseUpdater>()?.RunUpdate(repositories);
        }

        protected virtual void Configure()
        {

        }

        protected virtual void ConfigureServices(IServiceCollection services)
        {

        }
    }

    public static class Ioc
    {
        public static IServiceProvider Default { get; internal set; }
    }
}
