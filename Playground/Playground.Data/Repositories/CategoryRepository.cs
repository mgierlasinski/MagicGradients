using LiteDB;
using Playground.Data.Infrastructure;
using Playground.Data.Models;
using System.Collections.Generic;
using System.Linq;
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
                return collection.FindAll().OrderBy(x => x.Id);
            }
        }

        public IEnumerable<Theme> GetThemes()
        {
            using (var db = _databaseProvider.CreateDatabase())
            {
                var collection = db.GetCollection<Theme>(nameof(Theme));
                return collection.FindAll().OrderBy(x => x.Id);
            }
        }

        public void UpdateDatabase(LiteDatabase db, Metadata metadata, IDocumentRepository documentRepository)
        {
            InsertData<Category>(db, documentRepository, metadata.NameSpace, metadata.Categories);
            InsertData<Theme>(db, documentRepository, metadata.NameSpace, metadata.Themes);
        }

        private void InsertData<T>(LiteDatabase db, IDocumentRepository documentRepository, string nameSpace, string fileName)
        {
            var collection = db.GetCollection<T>(typeof(T).Name);
            collection.Delete(Query.All());

            var documents = documentRepository.GetDocumentCollection<T>(nameSpace, fileName);
            collection.InsertBulk(documents);
        }
    }
}
