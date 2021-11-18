using GradientsApp.Infrastructure;
using GradientsApp.Models;
using MagicGradients;
using MagicGradients.Converters;
using MvvmHelpers;
using Playground.Data.Infrastructure;
using Playground.Data.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace GradientsApp.ViewModels
{
    public class GalleryViewModel : ObservableObject, INavigationAware<CategoryItem>, IHaveTitle
    {
        private readonly INavigationService _navigationService;
        private readonly DimensionsTypeConverter _dimensionsConverter = new DimensionsTypeConverter();

        private string _title;
        public string Title
        {
            get => _title;
            private set => SetProperty(ref _title, value);
        }

        private List<GalleryItem> _galleryItems;
        public List<GalleryItem> GalleryItems
        {
            get => _galleryItems;
            set => SetProperty(ref _galleryItems, value);
        }

        private GalleryItem _selectedItem;
        public GalleryItem SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value, onChanged: () =>
            {
                if (_selectedItem == null)
                    return;

                _navigationService.NavigateTo(AppRoutes.Gradient, _selectedItem);
            });
        }

        public GalleryViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public void Prepare(CategoryItem parameter)
        {
            Title = parameter.Name;
            LoadGallery(parameter.Tag);
        }

        private void LoadGallery(string tag)
        {
            var repository = new GradientRepository(new DatabaseProvider());

            GalleryItems = repository.GetByTag(tag).Select(x => new GalleryItem
            {
                Source = new CssGradientSource(x.Stylesheet),
                Size = (Dimensions)_dimensionsConverter.ConvertFromInvariantString(x.Size)
            }).ToList();
        }
    }
}
