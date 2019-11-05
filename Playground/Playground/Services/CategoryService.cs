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
            var categories = _categoryRepository.GetCategories().OrderBy(x => x.Order).ToArray();
            var previews = _gradientRepository.GetBySlugs(categories.Select(x => x.Slug).ToArray());

            return categories.Select(x => MapCategory(x, previews));
        }

        public IEnumerable<GradientTheme> GetThemes()
        {
            return new[]
            {
                new GradientTheme(Color.FromRgb(235, 65, 65), "red"),
                new GradientTheme(Color.FromRgb(235, 150, 65), "orange"),
                new GradientTheme(Color.FromRgb(232, 235, 65), "yellow"),
                new GradientTheme(Color.FromRgb(77, 235, 65), "green"),
                new GradientTheme(Color.FromRgb(65, 187, 235), "blue"),
                new GradientTheme(Color.FromRgb(102, 65, 235), "violet"),
                new GradientTheme(Color.FromRgb(226, 65, 235), "pink")
            };
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
    }
}
