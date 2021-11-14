using GradientsApp.Forms;
using PlaygroundMaui.Pages;
using Xamarin.Forms;

namespace PlaygroundMaui
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new HomePage());
            new FormsApp().ConfigureAndRun();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
