using Playground.Features.Gallery.Models;
using Playground.Features.Gallery.Services;
using Playground.ViewModels;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Playground.Features.Gallery
{
    public class GalleryCategoriesViewModel : ObservableObject
    {
        private readonly ICategoryService _categoryService;

        private IEnumerable<CategoryGroup> _groups;
        public IEnumerable<CategoryGroup> Groups
        {
            get => _groups;
            set => SetProperty(ref _groups, value);
        }

        private CategoryItem _selectedItem;
        public CategoryItem SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value, async () =>
            {
                if (_selectedItem == null)
                    return;

                var tag = SelectedItem?.Tag ?? string.Empty;
                await Shell.Current.GoToAsync($"GalleryList?tag={tag}");
            });
        }

        public GalleryCategoriesViewModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public void LoadCategories()
        {
            Groups = _categoryService.GetGroupedCategories();
        }
    }
}
