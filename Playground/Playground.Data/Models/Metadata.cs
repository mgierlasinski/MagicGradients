using LiteDB;

namespace Playground.Data.Models
{
    public class Metadata
    {
        [BsonField("version")]
        public ushort Version { get; set; }

        [BsonField("namespace")]
        public string NameSpace { get; set; }

        [BsonField("categories")]
        public string Categories { get; set; }

        [BsonField("themes")]
        public string Themes { get; set; }

        [BsonField("gradients")]
        public string[] Gradients { get; set; }
    }
}
