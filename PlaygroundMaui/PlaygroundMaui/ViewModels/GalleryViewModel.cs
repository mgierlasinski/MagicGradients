using Playground.Data.Infrastructure;
using Playground.Data.Repositories;
using PlaygroundMaui.Infrastructure;
using PlaygroundMaui.Models;
using System.Collections.Generic;
using System.Linq;

namespace PlaygroundMaui.ViewModels
{
    public class GalleryViewModel : ObservableObject, INavigationAware<CategoryItem>
    {
        public string Name { get; private set; }

        public List<GalleryItem> GalleryItems { get; private set; }

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
                Stylesheet = x.Stylesheet,
                Size = x.Size
            }).ToList();

            RaisePropertyChanged(nameof(GalleryItems));
        }
    }
}
