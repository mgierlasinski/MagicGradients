using MagicGradients;
using Playground.Models;
using Playground.StaticData;
using System.Collections.Generic;
using System.Linq;
using Gradient = Playground.Models.Gradient;

namespace Playground.Services
{
    public class GalleryService : IGalleryService
    {
        public List<Gradient> GetGradients(string category)
        {
            var counter = 1;

            return StaticDataReader.ReadLines($"Playground.StaticData.Gradients.{category}.txt").Select(x =>
                new Gradient
                {
                    Id = counter++,
                    Source = new CssGradientSource {Stylesheet = x}
                }).ToList();
        }

        public Gradient GetGradientById(string category, int id)
        {
            return GetGradients(category).FirstOrDefault(x => x.Id == id);
        }
    }
}
