using Xamarin.Forms.Platform.UWP;

namespace Playground.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : WindowsPage
    {
        public MainPage()
        {
            this.InitializeComponent();
            LoadApplication(new Playground.App());
        }
    }
}
