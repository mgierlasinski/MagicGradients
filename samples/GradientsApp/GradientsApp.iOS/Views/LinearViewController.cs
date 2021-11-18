using Foundation;
using GradientsApp.iOS.Infrastructure;
using GradientsApp.ViewModels;

namespace GradientsApp.iOS.Views
{
    [Register("LinearViewController")]
    public class LinearViewController : BindableViewController<LinearViewModel>
    {
        public LinearViewController()
        {
        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Perform any additional setup after loading the view
        }
    }
}