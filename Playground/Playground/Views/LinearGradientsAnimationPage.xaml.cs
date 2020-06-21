using MagicGradients;
using Xamarin.Forms;

namespace Playground.Views
{
    public partial class LinearGradientsAnimationPage : ContentPage
    {
        public LinearGradientsAnimationPage()
        {
            InitializeComponent();

            AnimateRotate();
            AnimateScanner();
        }

        private void AnimateRotate()
        {
            var rotateAnimation = new Animation(v => RotateTarget.Angle = (float)v, 0, 360);
            rotateAnimation.Commit(this, "RotateAnimation", 16, 3000, repeat: () => true);
        }

        private void AnimateScanner()
        {
            var animationOffset = new Animation(v => ScannerTarget.Offset = new Offset(v, OffsetType.Proportional), 0, 1);
            var animationOffsetBack = new Animation(v => ScannerTarget.Offset = new Offset(v, OffsetType.Proportional), 1, 0);

            var parentAnimation = new Animation
            {
                {0, 0.5, animationOffset},
                {0.5, 1, animationOffsetBack}
            };

            parentAnimation.Commit(this, "ScannerAnimation", 16, 3000, repeat: () => true);
        }
    }
}