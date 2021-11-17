using Playground.Extensions;
using System.ComponentModel;
using Xamarin.Forms;

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
                this.InitSafeAreaInsets();
            }
        }
    }
}
