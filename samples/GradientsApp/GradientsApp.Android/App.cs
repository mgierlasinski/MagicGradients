using GradientsApp.Android.Infrastructure;
using GradientsApp.Android.Views;
using GradientsApp.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace GradientsApp.Android;

public class App
{
    public static void ConfigureAndRun()
    {
        Ioc.BuildServiceProvider(services =>
        {
            services.AddSingleton(CreateNavigation());
        });

        Ioc.ServiceProvider.GetService<AppStartup>()?.Run();
    }

    private static INavigationService CreateNavigation()
    {
        var navigation = new NavigationService();

        navigation.RegisterRoute(AppRoutes.Linear, typeof(LinearFragment));
        navigation.RegisterRoute(AppRoutes.Radial, typeof(RadialFragment));
        navigation.RegisterRoute(AppRoutes.Masks, typeof(MasksFragment));
        navigation.RegisterRoute(AppRoutes.Categories, typeof(CategoriesFragment));
        navigation.RegisterRoute(AppRoutes.Markup, typeof(MarkupFragment));

        return navigation;
    }
}