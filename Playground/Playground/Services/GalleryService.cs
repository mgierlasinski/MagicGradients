using MagicGradients;
using Playground.Data.Repositories;
using Playground.Models;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Gradient = Playground.Data.Models.Gradient;

namespace Playground.Services
{
    public class GalleryService : IGalleryService
    {
        private readonly IGradientRepository _gradientRepository;

        public GalleryService()
        {
            _gradientRepository = DependencyService.Get<IGradientRepository>();
        }

        public IEnumerable<GradientItem> GetGradients(string tag)
        {
            var x = _gradientRepository.GetByTag(tag);
            var y = x.Select(MapGradient);
             return y;
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
            Source = new CssGradientSource {Stylesheet = source.Stylesheet}
        };
    }
}
