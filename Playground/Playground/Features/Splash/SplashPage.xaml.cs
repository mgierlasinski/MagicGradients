using System.Threading.Tasks;
using MagicGradients;
using Playground.Features.Initializer;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using GradientStop = MagicGradients.GradientStop;

namespace Playground.Features.Splash
{
    public partial class SplashPage : ContentPage
    {
        public SplashPage()
        {
            InitializeComponent();
            BindingContext = true;
        }

        protected override void OnAppearing()
        {
            Task.Run(() =>
            {
                SetupIoC();
                Device.BeginInvokeOnMainThread(() => Application.Current.MainPage = new AppShell()); 
            });
        }

        private void SetupIoC()
        {
            AppSetup.ConfigureAndRun();
            
            if(Device.RuntimePlatform == Device.iOS)
                AppSetup.IoC.GetInstance<IPlatformInitializer>().Initialize();
            
            MagicGradients.Toolkit.Initializer.Init();
            Sharpnado.Shades.Initializer.Initialize(false);
            Sharpnado.Tabs.Initializer.Initialize(false, false);

        }
    }
}