using Foundation;
using UIKit;

namespace GradientsApp.iOS.Views
{
    [Register("ViewController")]
    public class ViewController : UIViewController
    {
        public ViewController()
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
            View = new GradientsView();

            base.ViewDidLoad();

            // Perform any additional setup after loading the view
        }
    }
}