using Xamarin.Forms;

namespace PlaygroundMaui.Pages
{
    public partial class GalleryPage
    {
        public GalleryPage()
        {
            InitializeComponent();
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GradientList.SelectedItem != null)
            {
                GradientList.SelectedItem = null;
            }
        }
    }
}