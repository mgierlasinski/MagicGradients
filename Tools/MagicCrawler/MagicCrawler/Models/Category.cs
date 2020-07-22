using System.Text.Json.Serialization;

namespace MagicCrawler.Models
{
    public class Category
    {
        public string Name { get; set; }
        public string Stylesheet { get; set; }
        public string Tag { get; set; }
        public string File { get; set; }

        [JsonIgnore]
        public int DisplayOrder { get; set; }
    }
}
