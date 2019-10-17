using Playground.Constants;
using Playground.Models;
using Playground.Services;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Playground.ViewModels
{
    public class GalleryListViewModel : BaseViewModel
    {
        private readonly IGalleryService _galleryService;

        public string[] Categories { get; } =
        {
            Category.Standard, Category.Angular,
            Category.Stripes, Category.Retro
        };

        private string _selectedCategory;
        public string SelectedCategory
        {
            get => _selectedCategory;
            set => SetProperty(ref _selectedCategory, value, onChanged: () =>
            {
                Gradients = _galleryService.GetGradients(SelectedCategory);
            });
        }

        private List<Gradient> _gradients;
        public List<Gradient> Gradients
        {
            get => _gradients;
            set => SetProperty(ref _gradients, value);
        }

        private Gradient _selectedItem;
        public Gradient SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value, onChanged: async () =>
            {
                if (_selectedItem == null)
                    return;

                var id = SelectedItem?.Id ?? 0;
                await Shell.Current.GoToAsync($"GalleryPreview?category={SelectedCategory}&id={id}");
            });
        }

        public GalleryListViewModel(IGalleryService galleryService)
        {
            _galleryService = galleryService;
            SelectedCategory = Category.Standard;
        }
    }
}
