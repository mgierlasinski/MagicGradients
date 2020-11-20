using System;
using Xamarin.Forms;

namespace Playground.Features.Gallery
{
    public partial class GalleryCategoriesPage : ContentPage
    {
        private bool _isLoaded;

        public GalleryCategoriesPage()
        {
            InitializeComponent();
            CategoriesList.SizeChanged += CategoriesListOnSizeChanged;
        }

        // TODO:
        // https://github.com/xamarin/Xamarin.Forms/issues/10977
        private void CategoriesListOnSizeChanged(object sender, EventArgs e)
        {
            if (!_isLoaded)
            {
                ((GalleryCategoriesViewModel)BindingContext).LoadCategories();
                _isLoaded = true;
            }

            var cols = (int)Math.Floor(CategoriesList.Width / 300);
            ((GridItemsLayout)CategoriesList.ItemsLayout).Span = cols;
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