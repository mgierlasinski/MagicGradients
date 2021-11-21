using Foundation;
using GradientsApp.iOS.Infrastructure;
using GradientsApp.ViewModels;

namespace GradientsApp.iOS.Views
{
    [Register("HomeViewController")]
    public class HomeViewController : BindableViewController<HomeViewModel>
    {
        public HomeViewController()
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