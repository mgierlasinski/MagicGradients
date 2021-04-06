using Xamarin.Forms;

namespace Playground
{
    public partial class App : Application
    {
        public App()
        {
            var _ = new MagicGradients.LinearGradient();
            var __ = new MagicGradients.Toolkit.Controls.MagicButton();

            Device.SetFlags(new[]
            {
                "Shapes_Experimental", 
                "Expander_Experimental", 
                "Shell_UWP_Experimental"
            });

            InitializeComponent();

            Sharpnado.Shades.Initializer.Initialize(false);
            Sharpnado.Tabs.Initializer.Initialize(false, false);

            AppSetup.RegisterTypes();
            AppSetup.RegisterViewModels();
            AppSetup.Initialize();

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
