using PlaygroundMaui.Infrastructure;
using PlaygroundMaui.Models;
using PlaygroundMaui.Resources;
using System.Collections.Generic;
using PlaygroundMaui.Pages;
using Xamarin.Forms;

namespace PlaygroundMaui.ViewModels
{
    public class CategoriesViewModel : ObservableObject
    {
        public List<Category> Categories { get; }

        private Category _selectedCategory;
        public Category SelectedCategory
        {
            get => _selectedCategory;
            set => SetProperty(ref _selectedCategory, value, async () =>
            {
                if (_selectedCategory == null)
                    return;

                ((App)Application.Current).Navigation.NavigateTo<GalleryPage, Category>(_selectedCategory);
            });
        }

        public CategoriesViewModel()
        {
            var reader = new DocumentReader();
            Categories = reader.GetDocument<List<Category>>("PlaygroundMaui.Resources.Categories.json");
        }
    }
}
