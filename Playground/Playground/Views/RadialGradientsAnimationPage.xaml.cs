using Xamarin.Forms;

namespace Playground.Views
{
    public partial class RadialGradientsAnimationPage : ContentPage
    {
        public RadialGradientsAnimationPage()
        {
            InitializeComponent();

            var animationXGrow = new Animation(v => AnimatedGradient.RadiusX = (float)v, 10, 300);
            var animationYGrow = new Animation(v => AnimatedGradient.RadiusY = (float)v, 10, 300);
            var animationXShrink = new Animation(v => AnimatedGradient.RadiusX = (float)v, 300, 10);
            var animationYShrink = new Animation(v => AnimatedGradient.RadiusY = (float)v, 300, 10);

            var parentAnimation = new Animation
            {
                {0, 0.5, animationXGrow},
                {0, 0.5, animationYGrow},
                {0.5, 1, animationXShrink},
                {0.5, 1, animationYShrink}
            };

            parentAnimation.Commit(this, "Animation", 30, 2000, repeat: () => true);
        }
    }
}