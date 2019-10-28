using LiteDB;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Playground.Data.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private const string NameSpace = "Playground.Data.Resources";

        private readonly string[] _files =
        {
            "Angular.json",
            "Burst.json",
            "Checkered.json",
            "Retro.json",
            "Standard.json",
            "Stripes.json"
        };

        public IEnumerable<BsonValue> GetInitialValues()
        {
            var documents = new List<BsonValue>();
            var assembly = typeof(GradientRepository).GetTypeInfo().Assembly;

            foreach (var file in _files)
            {
                using (var stream = assembly.GetManifestResourceStream($"{NameSpace}.{file}"))
                using (var reader = new StreamReader(stream))
                {
                    documents.AddRange(JsonSerializer.DeserializeArray(reader));
                }
            }

            return documents;
        }
    }
}
