using GradientsApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Playground.Data.Infrastructure;
using Playground.Data.Repositories;

namespace GradientsApp.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddServicesAndViewModels(this IServiceCollection services)
    {
        services.AddSingleton<IDatabaseProvider, DatabaseProvider>();
        services.AddSingleton<IDatabaseUpdater, DatabaseUpdater>();
        services.AddSingleton<IDocumentRepository, DocumentRepository>();
        services.AddSingleton<IGradientRepository, GradientRepository>();
        services.AddSingleton<ICategoryRepository, CategoryRepository>();
        services.AddSingleton<AppStartup>();

        services.AddTransient<HomeViewModel>();
        services.AddTransient<LinearViewModel>();
        services.AddTransient<CategoriesViewModel>();
        services.AddTransient<GalleryViewModel>();
        services.AddTransient<GradientViewModel>();
    }
}