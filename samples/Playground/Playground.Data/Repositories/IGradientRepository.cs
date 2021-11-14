using Playground.Data.Models;
using System.Collections.Generic;

namespace Playground.Data.Repositories
{
    public interface IGradientRepository : ICanUpdateMyself
    {
        Gradient GetById(int id);
        List<Gradient> GetByTag(string tag);
        List<Gradient> FilterByTags(string category, params string[] tags);
        List<Gradient> GetBySlugs(string[] slugs);
    }
}
