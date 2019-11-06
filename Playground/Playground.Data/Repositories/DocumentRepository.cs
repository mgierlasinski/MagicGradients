using LiteDB;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Playground.Data.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        public T GetDocument<T>(string fullPath)
        {
            var assembly = typeof(DocumentRepository).GetTypeInfo().Assembly;
            var mapper = BsonMapper.Global;

            using (var stream = assembly.GetManifestResourceStream(fullPath))
            using (var reader = new StreamReader(stream))
            {
                var value = JsonSerializer.Deserialize(reader);
                return mapper.ToObject<T>(value.AsDocument);
            }
        }

        public IEnumerable<T> GetDocumentCollection<T>(string nameSpace, string[] files)
        {
            var documents = new List<T>();
            var assembly = typeof(DocumentRepository).GetTypeInfo().Assembly;
            var mapper = BsonMapper.Global;

            foreach (var file in files)
            {
                using (var stream = assembly.GetManifestResourceStream($"{nameSpace}.{file}"))
                using (var reader = new StreamReader(stream))
                {
                    var array = JsonSerializer.DeserializeArray(reader).Select(x => mapper.ToObject<T>(x.AsDocument));
                    documents.AddRange(array);
                }
            }

            return documents;
        }
    }
}
