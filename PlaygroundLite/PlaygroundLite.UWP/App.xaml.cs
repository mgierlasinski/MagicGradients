using Sharpnado.Shades.UWP;
using Sharpnado.Tabs.Uwp;
using System.Reflection;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace PlaygroundLite.UWP
{
    sealed partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            if (rootFrame == null)
            {
                rootFrame = new Frame();
                var rendererAssemblies = new[]
                {
                    typeof(UWPShadowsRenderer).GetTypeInfo().Assembly,
                    typeof(UwpTintableImageEffect).GetTypeInfo().Assembly,
                };
                Xamarin.Forms.Forms.Init(e, rendererAssemblies);
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }
                Window.Current.Activate();
            }
        }
    }
}
