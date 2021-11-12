using GradientsApp.Android.Infrastructure;
using GradientsApp.Android.Views;
using GradientsApp.Infrastructure;
using MagicGradients;
using SimpleInjector;

namespace GradientsApp.Android
{
    public class App
    {
        public void Run()
        {
            GlobalSetup.Current.UseNativeGradients();

            ConfigureServices(AppSetup.IoC);

            AppSetup.ConfigureAndRun();
        }
        
        private void ConfigureServices(Container ioc)
        {
            var navigation = new NavigationService();
            navigation.RegisterRoute("Linear", typeof(LinearFragment));

            ioc.RegisterInstance<INavigationService>(navigation);
        }
    }
}