using MagicGradients;
using Playground.Data.Models;
using Playground.Data.Repositories;
using Playground.Models;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Gradient = Playground.Data.Models.Gradient;

namespace Playground.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IGradientRepository _gradientRepository;

        public CategoryService()
        {
            _categoryRepository = DependencyService.Get<ICategoryRepository>();
            _gradientRepository = DependencyService.Get<IGradientRepository>();
        }

        public IEnumerable<GradientCategory> GetCategories()
        {
            var categories = _categoryRepository.GetCategories().ToArray();
            return categories.Select(MapCategory);
        }

        public IEnumerable<GradientTheme> GetThemes()
        {
            return _categoryRepository.GetThemes().Select(MapTheme);
        }

        private GradientCategory MapCategory(Category source) => new GradientCategory
        {
            Name = source.Name,
            Tag = source.Tag,
            GradientSource = new CssGradientSource { Stylesheet = source.Stylesheet }
        };

        private GradientTheme MapTheme(Theme source) => new GradientTheme
        {
            ColorRaw = source.Color,
            Color = Color.FromHex(source.Color)
        };
    }
}
