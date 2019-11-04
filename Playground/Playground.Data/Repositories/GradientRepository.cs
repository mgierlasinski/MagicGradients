using LiteDB;
using Playground.Data.Infrastructure;
using Playground.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Playground.Data.Repositories
{
    public class GradientRepository : IGradientRepository
    {
        private readonly IDatabaseProvider _databaseProvider;
        private readonly IDocumentRepository _documentRepository;
        
        public GradientRepository()
        {
            _databaseProvider = DependencyService.Get<IDatabaseProvider>();
            _documentRepository = DependencyService.Get<IDocumentRepository>();
        }

        public void Initialize()
        {
            using (var db = _databaseProvider.CreateDatabase())
            {
                var collection = db.GetCollection<Gradient>(nameof(Gradient));

                if (collection.Count() > 0)
                {
                    collection.Delete(Query.All());
                }

                var documents = _documentRepository.GetInitialValues();
                var mapper = BsonMapper.Global;

                collection.InsertBulk(documents.Select(x => mapper.ToObject<Gradient>(x.AsDocument)));
                collection.EnsureIndex(x => x.Tags);
            }
        }

        public IEnumerable<Gradient> GetAll()
        {
            using (var db = _databaseProvider.CreateDatabase())
            {
                var collection = db.GetCollection<Gradient>(nameof(Gradient));
                return collection.FindAll().ToList();
            }
        }

        public Gradient GetById(Guid id)
        {
            using (var db = _databaseProvider.CreateDatabase())
            {
                var collection = db.GetCollection<Gradient>(nameof(Gradient));
                return collection.FindById(new BsonValue(id));
            }
        }

        public IEnumerable<Gradient> GetByTag(string tag)
        {
            using (var db = _databaseProvider.CreateDatabase())
            {
                var collection = db.GetCollection<Gradient>(nameof(Gradient));
                return collection.Find(x => x.Tags.Contains(tag)).ToList();
            }
        }

        public IEnumerable<Gradient> FilterByTags(string category, params string[] tags)
        {
            using (var db = _databaseProvider.CreateDatabase())
            {
                var collection = db.GetCollection<Gradient>(nameof(Gradient));
                return collection
                    .Find(x => x.Tags.Contains(category))
                    .Where(x => x.Tags.Intersect(tags).Any())
                    .ToList();
            }
        }

        public IEnumerable<Gradient> GetPreviewsForTags(string[] tags)
        {
            using (var db = _databaseProvider.CreateDatabase())
            {
                var collection = db.GetCollection<Gradient>(nameof(Gradient));
                var result = new List<Gradient>();

                foreach (var tag in tags)
                {
                    result.Add(collection.FindOne(x => x.Tags.Contains(tag) && x.IsPreview));
                }

                return result;
            }
        }
    }
}
