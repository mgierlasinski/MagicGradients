using LiteDB;

namespace Playground.Data.Models
{
    public class Category
    {
        public int Id { get; set; }

        [BsonField("name")]
        public string Name { get; set; }

        [BsonField("stylesheet")]
        public string Stylesheet { get; set; }

        [BsonField("tag")]
        public string Tag { get; set; }

        [BsonField("file")]
        public string File { get; set; }
    }
}
