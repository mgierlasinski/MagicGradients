using Playground.Models;
using Playground.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
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
                _allGradients = _galleryService.GetGradients(_categoryTag).ToList();
                RefreshGradients();
            });
        }

        private List<GradientTheme> _themes;
        public List<GradientTheme> Themes
        {
            get => _themes;
            set => SetProperty(ref _themes, value);
        }

        private GradientTheme _selectedTheme;
        public GradientTheme SelectedTheme
        {
            get => _selectedTheme;
            set => SetProperty(ref _selectedTheme, value, onChanged: RefreshGradients);
        }

        private ObservableCollection<object> _selectedThemes = new ObservableCollection<object>();
        public ObservableCollection<object> SelectedThemes
        {
            get => _selectedThemes;
            set => SetProperty(ref _selectedThemes, value);
        }

        private List<GradientItem> _allGradients;

        private List<GradientItem> _gradients;
        public List<GradientItem> Gradients
        {
            get => _gradients;
            set => SetProperty(ref _gradients, value);
        }

        private GradientItem _selectedItem;
        public GradientItem SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value, onChanged: async () =>
            {
                if (_selectedItem == null)
                    return;

                var id = SelectedItem?.Id ?? 0;
                await Shell.Current.GoToAsync($"GalleryPreview?id={id}");
            });
        }

        private bool _isPickerVisible;
        public bool IsPickerVisible
        {
            get => _isPickerVisible;
            set => SetProperty(ref _isPickerVisible, value);
        }

        public ICommand TogglePickerCommand { get; }

        public GalleryListViewModel(
            IGalleryService galleryService, 
            ICategoryService categoryService)
        {
            _galleryService = galleryService;
            _categoryService = categoryService;

            Themes = categoryService.GetThemes().ToList();
            SelectedThemes.CollectionChanged += SelectedThemesOnCollectionChanged;

            TogglePickerCommand = new Command(() => IsPickerVisible = !IsPickerVisible);
        }

        private void SelectedThemesOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RefreshGradients();
        }

        private void RefreshGradients()
        {
            if (SelectedThemes.Any())
            {
                var colors = SelectedThemes.Cast<GradientTheme>().Select(x => x.Color).ToArray();
                Gradients = _allGradients.Where(x => x.HasColors(colors)).ToList();
            }
            else if(SelectedTheme != null)
            {
                var colors = new[] { SelectedTheme.Color };
                Gradients = _allGradients.Where(x => x.HasColors(colors)).ToList();
            }
            else
            {
                Gradients = _allGradients;
            }
        }
    }
}
