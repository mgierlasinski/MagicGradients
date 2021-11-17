using GradientsApp.Forms.Pages;
using Xamarin.Forms;

namespace GradientsApp.Forms
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
