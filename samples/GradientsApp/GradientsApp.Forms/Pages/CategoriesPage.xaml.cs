﻿using Xamarin.Forms;

namespace GradientsApp.Forms.Pages
{
    public partial class CategoriesPage
    {
        public CategoriesPage()
        {
            InitializeComponent();
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CategoriesList.SelectedItem != null)
            {
                CategoriesList.SelectedItem = null;
            }
        }
    }
}