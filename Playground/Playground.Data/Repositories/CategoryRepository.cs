using LiteDB;
using Playground.Data.Infrastructure;
using Playground.Data.Models;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Playground.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IDatabaseProvider _databaseProvider;

        public CategoryRepository()
        {
            _databaseProvider = DependencyService.Get<IDatabaseProvider>();
        }

        public IEnumerable<Category> GetCategories()
        {
            using (var db = _databaseProvider.CreateDatabase())
            {
                var collection = db.GetCollection<Category>(nameof(Category));
                return collection.FindAll();
            }
        }

        public void UpdateDatabase(LiteDatabase db, Metadata metadata, IDocumentRepository documentRepository)
        {
            var collection = db.GetCollection<Category>(nameof(Category));

            if (collection.Count() > 0)
            {
                collection.Delete(Query.All());
            }

            var documents = documentRepository.GetDocumentCollection<Category>(metadata.NameSpace, new [] { metadata.Categories } );

            collection.InsertBulk(documents);
        }
    }
}
