using GradientsApp.Infrastructure;
using GradientsApp.Maui.Infrastructure;

namespace GradientsApp.Maui
{
    public static class MauiBuilderExtensions
    {
        public static MauiAppBuilder ConfigureServices(this MauiAppBuilder builder)
        {
            new AppSetupNew().Setup(builder.Services);

            builder.Services.AddSingleton<INavigationService, NavigationService>();

            return builder;
        }
    }
}
