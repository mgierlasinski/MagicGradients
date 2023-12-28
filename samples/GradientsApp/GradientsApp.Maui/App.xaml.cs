namespace GradientsApp.Maui
{
    public partial class App : Application
    {
        public App(AppStartup startup)
        {
            InitializeComponent();

            startup.Run();
            MainPage = new AppShell();
        }
    }
}
