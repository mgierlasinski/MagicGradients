using Playground.Views;
using Xamarin.Forms;

namespace Playground
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("GalleryPreview", typeof(GalleryPreviewPage));
            Routing.RegisterRoute("PasteCss", typeof(PasteCssPage));
        }
    }
}
