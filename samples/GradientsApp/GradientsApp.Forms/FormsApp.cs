using GradientsApp.Forms.Infrastructure;
using GradientsApp.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using PlaygroundMaui.Pages;

namespace GradientsApp.Forms
{
    public class FormsApp : AppSetup
    {
        protected override void ConfigureServices(IServiceCollection services)
        {
            var navigation = new NavigationService();
            SetupRoutes(navigation);

            services.AddSingleton<INavigationService>(navigation);
        }

        private void SetupRoutes(INavigationService navigation)
        {
            navigation.RegisterRoute(AppRoutes.Linear, typeof(LinearPage));
            navigation.RegisterRoute(AppRoutes.Radial, typeof(RadialPage));
            navigation.RegisterRoute(AppRoutes.Masks, typeof(MasksPage));
            navigation.RegisterRoute(AppRoutes.Animations, typeof(AnimationsPage));
            navigation.RegisterRoute(AppRoutes.Categories, typeof(CategoriesPage));
            navigation.RegisterRoute(AppRoutes.Gallery, typeof(GalleryPage));
            navigation.RegisterRoute(AppRoutes.Gradient, typeof(GradientPage));
        }
    }
}
