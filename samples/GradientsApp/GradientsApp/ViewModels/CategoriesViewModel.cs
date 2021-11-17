using GradientsApp.Infrastructure;
using GradientsApp.Models;
using MagicGradients;
using MvvmHelpers;
using Playground.Data.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace GradientsApp.ViewModels
{
    public class CategoriesViewModel : ObservableObject, INavigationAware
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly INavigationService _navigationService;

        public List<CategoryItem> Categories { get; private set; }

        private CategoryItem _selectedCategory;
        public CategoryItem SelectedCategory
        {
            get => _selectedCategory;
            set => SetProperty(ref _selectedCategory, value, onChanged: () =>
            {
                if (_selectedCategory == null)
                    return;

                _navigationService.NavigateTo(AppRoutes.Gallery, _selectedCategory);
            });
        }

        public CategoriesViewModel(
            ICategoryRepository categoryRepository, 
            INavigationService navigationService)
        {
            _categoryRepository = categoryRepository;
            _navigationService = navigationService;
        }
        
        public void Prepare()
        {
            LoadCategories(_categoryRepository);
        }

        private void LoadCategories(ICategoryRepository repository)
        {
            Categories = repository.GetCategories().Select(x => new CategoryItem
            {
                Name = x.Name,
                Source = new CssGradientSource(x.Stylesheet),
                Tag = x.Tag
            }).ToList();

            OnPropertyChanged(nameof(Categories));
        }
    }
}
