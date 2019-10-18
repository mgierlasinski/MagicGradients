using MagicGradients;
using Playground.Data.Repositories;
using Playground.StaticData;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Gradient = Playground.Models.Gradient;

namespace Playground.Services
{
    public class GalleryService : IGalleryService
    {
        private readonly IGradientRepository _gradientRepository;

        public GalleryService()
        {
            _gradientRepository = DependencyService.Get<IGradientRepository>();
            _gradientRepository.Initialize();
        }

        public List<Gradient> GetGradients(string category)
        {
            var counter = 1;

            return _gradientRepository.GetAll().Select(x => new Gradient
            {
                Id = counter++,
                Source = new CssGradientSource {Stylesheet = x.Stylesheet}
            }).ToList();

            //return StaticDataReader.ReadLines($"Playground.StaticData.Gradients.{category}.txt").Select(x =>
            //    new Gradient
            //    {
            //        Id = counter++,
            //        Source = new CssGradientSource {Stylesheet = x}
            //    }).ToList();
        }

        public Gradient GetGradientById(string category, int id)
        {
            return GetGradients(category).FirstOrDefault(x => x.Id == id);
        }
    }
}
