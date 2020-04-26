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
            var previews = _gradientRepository.GetBySlugs(categories.Select(x => x.Slug).ToArray());

            return categories.Select(x => MapCategory(x, previews));
        }

        public IEnumerable<GradientTheme> GetThemes()
        {
            return _categoryRepository.GetThemes().Select(MapTheme);
        }

        private GradientCategory MapCategory(Category source, IEnumerable<Gradient> previews) => new GradientCategory
        {
            Name = source.Name,
            Tag = source.Tag,
            GradientSource = new CssGradientSource
            {
                Stylesheet = previews.FirstOrDefault(x => x.Slug == source.Slug)?.Stylesheet ?? string.Empty
            }
        };

        private GradientTheme MapTheme(Theme source) => new GradientTheme
        {
            ColorRaw = source.Color,
            Color = Color.FromHex(source.Color)
        };
    }
}
