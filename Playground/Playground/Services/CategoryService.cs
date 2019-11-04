using MagicGradients;
using Playground.Constants;
using Playground.Data.Repositories;
using Playground.Models;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Playground.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IGradientRepository _gradientRepository;

        public CategoryService()
        {
            _gradientRepository = DependencyService.Get<IGradientRepository>();
        }

        public IEnumerable<GradientCategory> GetCategories()
        {
            var categories = new[]
            {
                new GradientCategory(Category.Standard, "standard"),
                new GradientCategory(Category.Angular, "angular"),
                new GradientCategory(Category.Stripes, "stripes"),
                new GradientCategory(Category.Retro, "retro"),
                new GradientCategory(Category.Checkered, "checkered"),
                new GradientCategory(Category.Burst, "burst")
            };

            foreach (var cat in categories)
            {
                var previews = _gradientRepository.GetPreviewsForTags(categories.Select(x => x.Tag).ToArray());

                cat.GradientSource = new CssGradientSource
                {
                    Stylesheet = previews.FirstOrDefault(x => x.Tags.Contains(cat.Tag))?.Stylesheet ?? string.Empty
                };
            }

            return categories;
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
    }
}
