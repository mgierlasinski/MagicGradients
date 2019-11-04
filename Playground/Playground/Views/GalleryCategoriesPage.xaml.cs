using Xamarin.Forms;

namespace Playground.Views
{
    public partial class GalleryCategoriesPage : ContentPage
    {
        public GalleryCategoriesPage()
        {
            InitializeComponent();
        }

        private void GalleryList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CategoriesList.SelectedItem != null)
            {
                CategoriesList.SelectedItem = null;
            }
        }
    }
}