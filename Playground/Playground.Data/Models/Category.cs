using LiteDB;
using System;

namespace Playground.Data.Models
{
    public class Category
    {
        public Guid Id { get; set; }

        [BsonField("name")]
        public string Name { get; set; }

        [BsonField("tag")]
        public string Tag { get; set; }

        [BsonField("order")]
        public ushort Order { get; set; }

        [BsonField("slug")]
        public string Slug { get; set; }
    }
}
