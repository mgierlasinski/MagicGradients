using MagicGradients;
using MagicGradients.Xaml;
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
    public class GalleryViewModel : ObservableObject, INavigationAware<CategoryItem>
    {
        private readonly DimensionsTypeConverter _dimensionsConverter = new DimensionsTypeConverter();

        public string Name { get; private set; }

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
            set => SetProperty(ref _selectedItem, value, () =>
            {
                if (_selectedItem == null)
                    return;

                App.Current.Navigation.NavigateTo<GradientPage, GalleryItem>(_selectedItem);
            });
        }

        public void Prepare(CategoryItem parameter)
        {
            Name = parameter.Name;
            LoadGallery(parameter.Tag);
        }

        private void LoadGallery(string tag)
        {
            var repository = new GradientRepository(new DatabaseProvider());
            GalleryItems = repository.GetByTag(tag).Select(x => new GalleryItem
            {
                Source = CssGradientSource.Parse(x.Stylesheet),
                Size = (Dimensions)_dimensionsConverter.ConvertFromInvariantString(x.Size)
            }).ToList();
        }
    }
}
