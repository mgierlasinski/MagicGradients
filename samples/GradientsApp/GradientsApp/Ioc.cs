using GradientsApp.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace GradientsApp;

public static class Ioc
{
    public static IServiceProvider ServiceProvider { get; internal set; }

    public static void BuildServiceProvider(Action<IServiceCollection> configure)
    {
        var services = new ServiceCollection();
        services.AddServicesAndViewModels();
        configure.Invoke(services);

        ServiceProvider = services.BuildServiceProvider();
    }
}