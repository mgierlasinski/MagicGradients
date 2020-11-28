using Xamarin.Forms;

namespace Playground.Features.Animation
{
    public partial class AnimationsPage : ContentPage
    {
        public AnimationsPage()
        {
            InitializeComponent();
        }

        protected override void OnDisappearing()
        {
            ((AnimationsViewModel)BindingContext).StopAnimations();
        }
    }
}