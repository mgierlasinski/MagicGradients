using Playground.Models;
using Playground.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Playground.ViewModels
{
    [QueryProperty("SelectedTag", "tag")]
    public class GalleryListViewModel : BaseViewModel
    {
        private readonly IGalleryService _galleryService;

        private string _selectedTag;
        public string SelectedTag
        {
            get => _selectedTag;
            set => SetProperty(ref _selectedTag, value, onChanged: () =>
            {
                Gradients = _galleryService.GetGradients(_selectedTag).ToList();
                Title = _galleryService.GetCategories().FirstOrDefault(x => x.Tag == _selectedTag)?.Name;
            });
        }

        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
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

                var id = SelectedItem?.Id ?? Guid.Empty;
                await Shell.Current.GoToAsync($"GalleryPreview?id={id}");
            });
        }

        public GalleryListViewModel(IGalleryService galleryService)
        {
            _galleryService = galleryService;
        }
    }
}
