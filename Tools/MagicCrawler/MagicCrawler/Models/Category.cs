using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MagicCrawler.Models
{
    public class Category
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("tag")]
        public string Tag { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }
    }
}
