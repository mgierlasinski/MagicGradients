using LiteDB;
using Playground.Data.Infrastructure;
using Playground.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace Playground.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IDatabaseProvider _databaseProvider;

        public CategoryRepository(IDatabaseProvider databaseProvider)
        {
            _databaseProvider = databaseProvider;
        }

        public List<Category> GetCategories()
        {
            using (var db = _databaseProvider.CreateDatabase())
            {
                var collection = db.GetCollection<Category>(nameof(Category));
                var result = collection.FindAll()
                    .OrderBy(x => x.Id)
                    .ToList();

                var gradients = db.GetCollection<Gradient>();

                foreach (var cat in result)
                {
                    cat.Count = gradients.Count(x => x.Tags.Contains(cat.Tag));
                }

                return result;
            }
        }

        public List<Theme> GetThemes()
        {
            using (var db = _databaseProvider.CreateDatabase())
            {
                var collection = db.GetCollection<Theme>(nameof(Theme));
                return collection.FindAll().OrderBy(x => x.Id).ToList();
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
            collection.DeleteAll();

            var documents = documentRepository.GetDocumentCollection<T>(nameSpace, fileName);
            collection.InsertBulk(documents);
        }
    }
}
