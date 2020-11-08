using Playground.Features.BattleTest;
using Playground.Features.CssPreviewer;
using Playground.Features.Gallery;
using Playground.Views;
using Xamarin.Forms;

namespace Playground
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("GalleryList", typeof(GalleryListPage));
            Routing.RegisterRoute("GalleryPreview", typeof(GalleryPreviewPage));
            Routing.RegisterRoute("CssPreviewer", typeof(CssPreviewerPage));
            Routing.RegisterRoute("BattleTest", typeof(BattleTestPage));
        }
    }
}
