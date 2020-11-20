using System;
using Xamarin.Forms;

namespace Playground.Features.Gallery
{
    public partial class GalleryListPage : ContentPage
    {
        private bool _isLoaded;

        public GalleryListPage()
        {
            InitializeComponent();
            GalleryList.SizeChanged += GalleryListOnSizeChanged;
        }

        // TODO:
        // https://github.com/xamarin/Xamarin.Forms/issues/10977
        private void GalleryListOnSizeChanged(object sender, EventArgs e)
        {
            if (!_isLoaded)
            {
                ((GalleryListViewModel)BindingContext).LoadGradients();
                _isLoaded = true;
            }

            var cols = (int)Math.Floor(GalleryList.Width / 300);
            ((GridItemsLayout) GalleryList.ItemsLayout).Span = cols;
        }

        private void GalleryList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GalleryList.SelectedItem != null)
            {
                GalleryList.SelectedItem = null;
            }
        }
    }
}