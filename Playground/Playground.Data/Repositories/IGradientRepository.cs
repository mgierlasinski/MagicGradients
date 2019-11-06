using Playground.Data.Models;
using System.Collections.Generic;

namespace Playground.Data.Repositories
{
    public interface IGradientRepository : ICanUpdateMyself
    {
        Gradient GetById(int id);

        IEnumerable<Gradient> GetByTag(string tag);

        IEnumerable<Gradient> FilterByTags(string category, params string[] tags);

        IEnumerable<Gradient> GetBySlugs(string[] slugs);
    }
}
