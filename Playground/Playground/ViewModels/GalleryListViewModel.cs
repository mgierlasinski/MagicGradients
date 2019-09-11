using Playground.Models;
using Playground.Services;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Playground.ViewModels
{
    public class GalleryListViewModel : BaseViewModel
    {
        public List<Gradient> Gradients { get; set; }

        private Gradient _selectedItem;
        public Gradient SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value, onChanged: async () =>
            {
                await Shell.Current.GoToAsync("//gallery/preview");
            });
        }

        public GalleryListViewModel()
        {
            var service = DependencyService.Get<IGalleryService>();
            Gradients = service.GetGradients();
        }
    }
}
