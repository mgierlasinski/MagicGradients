using GradientsApp.Infrastructure;
using GradientsApp.iOS.Infrastructure;
using GradientsApp.iOS.Views;
using MagicGradients;
using Microsoft.Extensions.DependencyInjection;

namespace GradientsApp.iOS
{
    public class App : AppSetup
    {
        protected override void Configure()
        {
            GlobalSetup.Current.UseNativeGradients();
        }

        protected override void ConfigureServices(IServiceCollection services)
        {
            var navigation = new NavigationService();
            SetupRoutes(navigation);

            services.AddSingleton<INavigationService>(navigation);
        }

        private void SetupRoutes(INavigationService navigation)
        {
            navigation.RegisterRoute(AppRoutes.Linear, typeof(LinearViewController));
        }
    }
}