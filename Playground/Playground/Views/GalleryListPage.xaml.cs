using Xamarin.Forms;

namespace Playground.Views
{
    public partial class GalleryListPage : ContentPage
    {
        public GalleryListPage()
        {
            InitializeComponent();
        }

        private void GalleryList_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (GalleryList.SelectedItem != null)
            {
                GalleryList.SelectedItem = null;
            }
        }
    }
}