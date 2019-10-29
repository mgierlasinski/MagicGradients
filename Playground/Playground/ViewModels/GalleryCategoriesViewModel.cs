using Playground.Models;
using Playground.Services;
using System.Collections.Generic;

namespace Playground.ViewModels
{
    public class GalleryCategoriesViewModel : BaseViewModel
    {
        private IEnumerable<GradientCategory> _categories;
        public IEnumerable<GradientCategory> Categories
        {
            get => _categories;
            set => SetProperty(ref _categories, value);
        }

        private GradientCategory _selectedItem;
        public GradientCategory SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value, onChanged: async () =>
            {
                //if (_selectedItem == null)
                //    return;

                //var id = SelectedItem?.Id ?? Guid.Empty;
                //await Shell.Current.GoToAsync($"GalleryPreview?id={id}");
            });
        }

        public GalleryCategoriesViewModel(IGalleryService galleryService)
        {
            Categories = galleryService.GetCategories();
        }
    }
}
