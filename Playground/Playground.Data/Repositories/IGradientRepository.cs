using Playground.Data.Models;
using System;
using System.Collections.Generic;

namespace Playground.Data.Repositories
{
    public interface IGradientRepository
    {
        void Initialize();

        IEnumerable<Gradient> GetAll();

        Gradient GetById(Guid id);

        IEnumerable<Gradient> GetByTag(string tag);

        IEnumerable<Gradient> GetPreviewsForTags(params string[] tags);
    }
}
