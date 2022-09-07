using System;
using LiteDB;

namespace Playground.Data.Models
{
    public class Metadata
    {
        [BsonField("date")]
        public DateTime Date { get; set; }

        [BsonField("nameSpace")]
        public string NameSpace { get; set; }

        [BsonField("categories")]
        public string Categories { get; set; }

        [BsonField("themes")]
        public string Themes { get; set; }
    }
}
