using Xamarin.Forms.Platform.UWP;

namespace PlaygroundLite.UWP
{
    public sealed partial class MainPage : WindowsPage
    {
        public MainPage()
        {
            InitializeComponent();
            LoadApplication(new PlaygroundLite.App());
        }
    }
}
