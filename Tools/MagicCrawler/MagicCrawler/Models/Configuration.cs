using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MagicCrawler.Models
{
    public class Configuration
    {
        [JsonProperty("input")]
        public string Input { get; set; }

        [JsonProperty("output")]
        public string Output { get; set; }

        [JsonProperty("endpoints")]
        public Endpoint[] Endpoints { get; set; }
    }
}
