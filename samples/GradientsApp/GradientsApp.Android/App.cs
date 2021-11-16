using GradientsApp.Android.Infrastructure;
using GradientsApp.Android.Views;
using GradientsApp.Infrastructure;
using MagicGradients;
using Microsoft.Extensions.DependencyInjection;

namespace GradientsApp.Android
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
            navigation.RegisterRoute(AppRoutes.Linear, typeof(LinearFragment));
            navigation.RegisterRoute(AppRoutes.Radial, typeof(RadialFragment));
            navigation.RegisterRoute(AppRoutes.Categories, typeof(CategoriesFragment));
        }
    }
}