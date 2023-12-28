using GradientsApp.Extensions;
using GradientsApp.Infrastructure;
using GradientsApp.Maui.Infrastructure;

namespace GradientsApp.Maui;

public static class MauiBuilderExtensions
{
    public static MauiAppBuilder ConfigureServices(this MauiAppBuilder builder)
    {
        builder.Services.AddServicesAndViewModels();
        builder.Services.AddSingleton<INavigationService, NavigationService>();

        return builder;
    }
}