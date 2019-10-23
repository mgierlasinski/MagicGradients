using MagicGradients;
using Playground.Data.Repositories;
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

        public List<Gradient> GetGradients(string category)
        {
            var tag = category.ToLowerInvariant();
            return _gradientRepository.GetByTag(tag).Select(MapGradient).ToList();
        }

        public Gradient GetGradientById(Guid id)
        {
            var result = _gradientRepository.GetById(id);
            return MapGradient(result);
        }

        private Gradient MapGradient(GradientDto source) => new Gradient
        {
            Id = source.Id,
            Source = new CssGradientSource {Stylesheet = source.Stylesheet}
        };
    }
}
