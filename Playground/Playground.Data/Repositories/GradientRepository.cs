using LiteDB;
using Playground.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Playground.Data.Repositories
{
    public class GradientRepository : IGradientRepository
    {
        private readonly string[] _files = { "Standard.json" };

        public void Initialize()
        {
            using (var db = new LiteDatabase(GetDbPath()))
            {
                var collection = db.GetCollection<Gradient>("gradients");

                if (collection.Count() > 0)
                {
                    collection.Delete(Query.All());
                }

                var documents = GetDocuments("Playground.Data.Resources", _files);
                var mapper = BsonMapper.Global;

                collection.InsertBulk(documents.Select(x => mapper.ToObject<Gradient>(x.AsDocument)));
                collection.EnsureIndex(x => x.Tags);
            }
        }

        public IEnumerable<Gradient> GetAll()
        {
            using (var db = new LiteDatabase(GetDbPath()))
            {
                var collection = db.GetCollection<Gradient>("gradients");
                return collection.FindAll().ToList();
            }
        }

        public Gradient GetById(Guid id)
        {
            using (var db = new LiteDatabase(GetDbPath()))
            {
                var collection = db.GetCollection<Gradient>("gradients");
                return collection.FindById(new BsonValue(id));
            }
        }

        public IEnumerable<Gradient> GetByTag(string tag)
        {
            using (var db = new LiteDatabase(GetDbPath()))
            {
                var collection = db.GetCollection<Gradient>("gradients");
                return collection.Find(x => x.Tags.Contains(tag)).ToList();
            }
        }

        private List<BsonValue> GetDocuments(string nameSpace, string[] files)
        {
            var documents = new List<BsonValue>();
            var assembly = typeof(GradientRepository).GetTypeInfo().Assembly;

            foreach (var file in files)
            {
                using (var stream = assembly.GetManifestResourceStream($"{nameSpace}.{file}"))
                using (var reader = new StreamReader(stream))
                {
                    documents.AddRange(JsonSerializer.DeserializeArray(reader));
                }
            }

            return documents;
        }

        private string GetDbPath()
        {
            var dir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            return Path.Combine(dir, "Gradients.db");
        }
    }
}
