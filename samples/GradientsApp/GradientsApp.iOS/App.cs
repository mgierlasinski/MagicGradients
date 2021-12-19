using GradientsApp.Infrastructure;
using GradientsApp.iOS.Infrastructure;
using GradientsApp.iOS.Views;
using Microsoft.Extensions.DependencyInjection;

namespace GradientsApp.iOS
{
    public class App : AppSetup
    {
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