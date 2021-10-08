using PlaygroundMaui.Infrastructure;
using PlaygroundMaui.Pages;
using Xamarin.Forms;

namespace PlaygroundMaui
{
    public partial class App : Application, INavigationHandler
    {
        public new static App Current { get; private set; }
        public INavigationService Navigation { get; }

        public App()
        {
            InitializeComponent();

            AppSetup.ConfigureAndRun();

            MainPage = new NavigationPage(new MainPage());
            Navigation = new NavigationService(MainPage);
            Current = this;
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
