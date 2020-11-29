using MagicGradients;
using Playground.Data.Models;
using Playground.Data.Repositories;
using Playground.Features.Gallery.Models;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Playground.Features.Gallery.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IEnumerable<CategoryItem> GetCategories()
        {
            var categories = _categoryRepository.GetCategories();
            return categories.Select(MapCategory);
        }

        public IEnumerable<CategoryGroup> GetGroupedCategories()
        {
            var result = new List<CategoryGroup>();
            var groups = _categoryRepository.GetGroupedCategories();

            foreach (var group in groups)
            {
                result.Add(new CategoryGroup(group.Key, group.Select(MapCategory)));
            }

            return result;
        }

        public IEnumerable<ThemeItem> GetThemes()
        {
            return _categoryRepository.GetThemes().Select(MapTheme);
        }

        private CategoryItem MapCategory(Category source) => new CategoryItem
        {
            Name = source.Name,
            Tag = source.Tag,
            Count = source.Count,
            GradientSource = new CssGradientSource { Stylesheet = source.Stylesheet }
        };

        private ThemeItem MapTheme(Theme source) => new ThemeItem
        {
            ColorRaw = source.Color,
            Color = Color.FromHex(source.Color)
        };
    }
}
