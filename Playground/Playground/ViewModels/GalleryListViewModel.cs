using Playground.Models;
using Playground.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
                LoadGradientsMultiple();
            });
        }

        private List<GradientTheme> _themes;
        public List<GradientTheme> Themes
        {
            get => _themes;
            set => SetProperty(ref _themes, value);
        }

        private ObservableCollection<object> _selectedThemes = new ObservableCollection<object>();
        public ObservableCollection<object> SelectedThemes
        {
            get => _selectedThemes;
            set => SetProperty(ref _selectedThemes, value);
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

        public GalleryListViewModel(
            IGalleryService galleryService, 
            ICategoryService categoryService)
        {
            _galleryService = galleryService;
            _categoryService = categoryService;

            Themes = categoryService.GetThemes().ToList();
            SelectedThemes.CollectionChanged += SelectedThemesOnCollectionChanged;
        }

        private void SelectedThemesOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            LoadGradientsMultiple();
        }

        private void LoadGradientsMultiple()
        {
            if (SelectedThemes.Any())
            {
                Gradients = _galleryService
                    .FilterGradients(_categoryTag, SelectedThemes
                        .Cast<GradientTheme>()
                        .Select(x => x.Tag).ToArray())
                    .ToList();
            }
            else
            {
                Gradients = _galleryService.GetGradients(_categoryTag).ToList();
            }
        }
    }
}
