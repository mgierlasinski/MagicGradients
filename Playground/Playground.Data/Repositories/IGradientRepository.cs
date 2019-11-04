using LiteDB;
using Playground.Data.Models;
using System;
using System.Collections.Generic;

namespace Playground.Data.Repositories
{
    public interface IGradientRepository
    {
        void UpdateDatabase(LiteDatabase database, Metadata metadata);

        Gradient GetById(Guid id);

        IEnumerable<Gradient> GetByTag(string tag);

        IEnumerable<Gradient> FilterByTags(string category, params string[] tags);

        IEnumerable<Gradient> GetPreviewsForTags(string[] tags);
    }
}
