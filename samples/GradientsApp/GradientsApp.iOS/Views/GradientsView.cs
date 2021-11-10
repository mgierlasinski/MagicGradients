using Foundation;
using GradientsApp.ViewModels;
using System.Drawing;
using UIKit;

namespace GradientsApp.iOS.Views
{
    [Register("GradientsView")]
    public class GradientsView : UIView
    {
        public GradientsView()
        {
            Initialize();
        }

        public GradientsView(RectangleF bounds) : base(bounds)
        {
            Initialize();
        }

        private void Initialize()
        {
            BackgroundColor = UIColor.Red;
            SetGradients();
        }

        private void SetGradients()
        {
            var vm = new GradientsViewModel();

            // TODO: setup GradientView
        }
    }
}