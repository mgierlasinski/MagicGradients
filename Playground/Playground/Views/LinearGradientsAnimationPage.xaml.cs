using Xamarin.Forms;

namespace Playground.Views
{
    public partial class LinearGradientsAnimationPage : ContentPage
    {
        public LinearGradientsAnimationPage()
        {
            InitializeComponent();

            var animationOffset = new Animation(v => AnimatedStop.Offset = (float)v, 0, 1);
            var animationOffsetBack = new Animation(v => AnimatedStop.Offset = (float)v, 1, 0);

            var parentAnimation = new Animation
            {
                {0, 0.5, animationOffset},
                {0.5, 1, animationOffsetBack}
            };

            parentAnimation.Commit(this, "Animation", 16, 3000, repeat: () => true);
        }
    }
}