using LiteDB;
using System;

namespace Playground.Data.Models
{
    public class Theme
    {
        public Guid Id { get; set; }

        [BsonField("color")]
        public string Color { get; set; }

        [BsonField("tag")]
        public string Tag { get; set; }

        [BsonField("order")]
        public ushort Order { get; set; }
    }
}
