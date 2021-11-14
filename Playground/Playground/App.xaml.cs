using Xamarin.Forms;

namespace Playground
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
#if WINDOWS_UWP
            Device.SetFlags(new[] { "Shell_UWP_Experimental" });
#endif

            MagicGradients.Toolkit.Initializer.Init();
            Sharpnado.Shades.Initializer.Initialize(false);
            Sharpnado.Tabs.Initializer.Initialize(false, false);

            AppSetup.ConfigureAndRun();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
