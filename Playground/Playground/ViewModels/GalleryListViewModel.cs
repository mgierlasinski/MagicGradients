using Playground.Models;
using Playground.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Playground.ViewModels
{
    [QueryProperty("CategoryTag", "tag")]
    public class GalleryListViewModel : BaseViewModel
    {
        private readonly IGalleryService _galleryService;
        private readonly ICategoryService _categoryService;

        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private string _categoryTag;
        public string CategoryTag
        {
            get => _categoryTag;
            set => SetProperty(ref _categoryTag, value, onChanged: () =>
            {
                Title = _categoryService.GetCategories().FirstOrDefault(x => x.Tag == _categoryTag)?.Name;
                LoadGradients();
            });
        }

        private List<GradientTheme> _themes;
        public List<GradientTheme> Themes
        {
            get => _themes;
            set => SetProperty(ref _themes, value);
        }

        private List<Gradient> _gradients;
        public List<Gradient> Gradients
        {
            get => _gradients;
            set => SetProperty(ref _gradients, value);
        }

        private GradientTheme _selectedTheme;
        public GradientTheme SelectedTheme
        {
            get => _selectedTheme;
            set => SetProperty(ref _selectedTheme, value, onChanged: LoadGradients);
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

        public GalleryListViewModel(
            IGalleryService galleryService, 
            ICategoryService categoryService)
        {
            _galleryService = galleryService;
            _categoryService = categoryService;

            Themes = categoryService.GetThemes().ToList();
        }

        private void LoadGradients()
        {
            if (SelectedTheme != null)
            {
                Gradients = _galleryService.FilterGradients(_categoryTag, SelectedTheme.Tag).ToList();
            }
            else
            {
                Gradients = _galleryService.GetGradients(_categoryTag).ToList();
            }
        }
    }
}
