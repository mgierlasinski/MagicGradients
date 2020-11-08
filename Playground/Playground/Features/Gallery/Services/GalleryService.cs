using MagicGradients;
using MagicGradients.Xaml;
using Playground.Data.Repositories;
using Playground.Features.Gallery.Models;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Gradient = Playground.Data.Models.Gradient;

namespace Playground.Features.Gallery.Services
{
    public class GalleryService : IGalleryService
    {
        private readonly IGradientRepository _gradientRepository;
        private readonly DimensionsTypeConverter _dimensionsConverter;

        public GalleryService()
        {
            _gradientRepository = DependencyService.Get<IGradientRepository>();
            _dimensionsConverter = new DimensionsTypeConverter();
        }

        public IEnumerable<GradientItem> GetGradients(string tag)
        {
            return _gradientRepository.GetByTag(tag).Select(MapGradient);
        }

        public IEnumerable<GradientItem> FilterGradients(string category, params string[] tags)
        {
            return _gradientRepository.FilterByTags(category, tags).Select(MapGradient);
        }

        public GradientItem GetGradientById(int id)
        {
            var result = _gradientRepository.GetById(id);
            return MapGradient(result);
        }

        private GradientItem MapGradient(Gradient source) => new GradientItem
        {
            Id = source.Id,
            Source = new CssGradientSource { Stylesheet = source.Stylesheet },
            Size = (Dimensions)_dimensionsConverter.ConvertFromInvariantString(source.Size)
        };
    }
}
