using Playground.Data.Infrastructure;
using Playground.Data.Repositories;
using PlaygroundMaui.Infrastructure;
using PlaygroundMaui.Models;
using PlaygroundMaui.Pages;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace PlaygroundMaui.ViewModels
{
    public class CategoriesViewModel : ObservableObject
    {
        public List<CategoryItem> Categories { get; private set; }

        private CategoryItem _selectedCategory;
        public CategoryItem SelectedCategory
        {
            get => _selectedCategory;
            set => SetProperty(ref _selectedCategory, value, async () =>
            {
                if (_selectedCategory == null)
                    return;

                ((App)Application.Current).Navigation.NavigateTo<GalleryPage, CategoryItem>(_selectedCategory);
            });
        }

        public CategoriesViewModel()
        {
            //var reader = new DocumentReader();
            //Categories = reader.GetDocument<List<CategoryItem>>("PlaygroundMaui.Resources.Categories.json");
            LoadCategories();
        }

        private void LoadCategories()
        {
            var repository = new CategoryRepository(new DatabaseProvider());
            Categories = repository.GetCategories().Select(x => new CategoryItem
            {
                Name = x.Name,
                Stylesheet = x.Stylesheet,
                Tag = x.Tag
            }).ToList();
        }
    }
}
