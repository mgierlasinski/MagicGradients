using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MagicCrawler.Models
{
    public class Gradient
    {
        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("stylesheet")]
        public string Stylesheet { get; set; }

        [JsonProperty("size")]
        public string Size { get; set; }

        [JsonProperty("tags")]
        public List<string> Tags { get; set; }
    }
}
