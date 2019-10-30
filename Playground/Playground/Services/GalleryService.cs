using MagicGradients;
using Playground.Constants;
using Playground.Data.Repositories;
using Playground.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Gradient = Playground.Models.Gradient;
using GradientDto = Playground.Data.Models.Gradient;

namespace Playground.Services
{
    public class GalleryService : IGalleryService
    {
        private readonly IGradientRepository _gradientRepository;

        public GalleryService()
        {
            _gradientRepository = DependencyService.Get<IGradientRepository>();
        }

        public IEnumerable<Gradient> GetGradients(string tag)
        {
            return _gradientRepository.GetByTag(tag).Select(MapGradient);
        }

        public Gradient GetGradientById(Guid id)
        {
            var result = _gradientRepository.GetById(id);
            return MapGradient(result);
        }

        public IEnumerable<GradientCategory> GetCategories()
        {
            var categories = new[]
            {
                new GradientCategory(Category.Standard),
                new GradientCategory(Category.Angular),
                new GradientCategory(Category.Stripes),
                new GradientCategory(Category.Retro),
                new GradientCategory(Category.Checkered),
                new GradientCategory(Category.Burst)
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

        private Gradient MapGradient(GradientDto source) => new Gradient
        {
            Id = source.Id,
            Source = new CssGradientSource {Stylesheet = source.Stylesheet}
        };
    }
}
