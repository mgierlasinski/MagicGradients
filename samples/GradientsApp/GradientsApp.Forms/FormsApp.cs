using GradientsApp.Forms.Infrastructure;
using GradientsApp.Forms.Pages;
using GradientsApp.Infrastructure;
using MagicGradients;
using MagicGradients.Forms;
using MagicGradients.Forms.Skia;
using Microsoft.Extensions.DependencyInjection;

namespace GradientsApp.Forms
{
    public class FormsApp : AppSetup
    {
        protected override void Configure()
        {
            GlobalSetup.Current
                .UseFormsGradients()
                .UseFormsSkiaGradients();
        }

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
            navigation.RegisterRoute(AppRoutes.Markup, typeof(MarkupPage));
        }
    }
}
