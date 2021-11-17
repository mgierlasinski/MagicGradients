using LiteDB;
using Playground.Data.Infrastructure;
using Playground.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace Playground.Data.Repositories
{
    public class GradientRepository : IGradientRepository
    {
        private readonly IDatabaseProvider _databaseProvider;
        
        public GradientRepository(IDatabaseProvider databaseProvider)
        {
            _databaseProvider = databaseProvider;
        }
        
        public Gradient GetById(int id)
        {
            using (var db = _databaseProvider.CreateDatabase())
            {
                var collection = db.GetCollection<Gradient>(nameof(Gradient));
                return collection.FindById(new BsonValue(id));
            }
        }

        public List<Gradient> GetByTag(string tag)
        {
            using (var db = _databaseProvider.CreateDatabase())
            {
                var collection = db.GetCollection<Gradient>(nameof(Gradient));
                return collection.Find(x => x.Tags.Contains(tag)).OrderBy(x => x.Id).ToList();
            }
        }

        public List<Gradient> FilterByTags(string category, params string[] tags)
        {
            using (var db = _databaseProvider.CreateDatabase())
            {
                var collection = db.GetCollection<Gradient>(nameof(Gradient));
                return collection
                    .Find(x => x.Tags.Contains(category))
                    .Where(x => x.Tags.Intersect(tags).Any()).ToList();
            }
        }

        public List<Gradient> GetBySlugs(string[] slugs)
        {
            using (var db = _databaseProvider.CreateDatabase())
            {
                var collection = db.GetCollection<Gradient>(nameof(Gradient));
                return collection.Find(x => slugs.Contains(x.Slug)).ToList();
            }
        }

        public void UpdateDatabase(LiteDatabase db, Metadata metadata, IDocumentRepository documentRepository)
        {
            var collection = db.GetCollection<Gradient>(nameof(Gradient));
            collection.DeleteAll();

            var categories = documentRepository.GetDocumentCollection<Category>(metadata.NameSpace, metadata.Categories);
            var gradients = documentRepository.GetDocumentCollection<Gradient>(metadata.NameSpace, categories.Select(x => x.File).ToArray());

            collection.InsertBulk(gradients);
            collection.EnsureIndex(x => x.Slug);
            collection.EnsureIndex(x => x.Tags);
        }
    }
}
