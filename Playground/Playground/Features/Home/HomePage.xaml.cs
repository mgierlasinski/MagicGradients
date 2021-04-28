using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Application = Xamarin.Forms.Application;

namespace Playground.Features.Home
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            PropertyChanged += ContentPageBase_PropertyChanged;
        }
        
        private void ContentPageBase_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SafeAreaInsets")
            {
                var safeInsets = On<iOS>().SafeAreaInsets();
                safeInsets.Top = 0;
                safeInsets.Bottom = safeInsets.Bottom * -1;
                Application.Current.Resources["SafeAreaInsets"] = safeInsets;
            }
        }
    }
}
