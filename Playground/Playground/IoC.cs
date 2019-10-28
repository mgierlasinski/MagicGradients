using Playground.Data.Infrastructure;
using Playground.Data.Repositories;
using Playground.Services;
using Xamarin.Forms;

namespace Playground
{
    public static class IoC
    {
        public static void Initialize()
        {
            DependencyService.Register<DatabaseProvider>();
            DependencyService.Register<DocumentRepository>();
            DependencyService.Register<GradientRepository>();
            DependencyService.Register<GalleryService>();

            DependencyService.Resolve<IGradientRepository>().Initialize();
        }
    }
}
