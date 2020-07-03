using LiteDB;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Playground.Data.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private const string DateTimeFormat = "yyyy-MM-ddTHH:mm:ss";

        public void SetupMapper()
        {
            BsonMapper.Global.ResolveMember = (type, memberInfo, member) =>
            {
                if (member.DataType == typeof(DateTime))
                {
                    member.Deserialize = (v, m) => DateTime.ParseExact(v.AsString, DateTimeFormat, CultureInfo.InvariantCulture);
                    member.Serialize = (o, m) => ((DateTime)o).ToString(DateTimeFormat);
                }
            };
        }

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

        public IEnumerable<T> GetDocumentCollection<T>(string nameSpace, params string[] files)
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
