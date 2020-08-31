using LiteDB;

namespace Playground.Data.Models
{
    public class Gradient
    {
        public int Id { get; set; }

        [BsonField("stylesheet")]
        public string Stylesheet { get; set; }

        [BsonField("size")]
        public string Size { get; set; }

        [BsonField("tags")]
        public string[] Tags { get; set; }

        [BsonField("slug")]
        public string Slug { get; set; }
    }
}