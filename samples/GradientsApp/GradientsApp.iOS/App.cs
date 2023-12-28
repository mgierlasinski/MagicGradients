using GradientsApp.Infrastructure;
using GradientsApp.iOS.Infrastructure;
using GradientsApp.iOS.Views;
using Microsoft.Extensions.DependencyInjection;

namespace GradientsApp.iOS;

public static class App
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

        navigation.RegisterRoute(AppRoutes.Linear, typeof(LinearViewController));

        return navigation;
    }
}