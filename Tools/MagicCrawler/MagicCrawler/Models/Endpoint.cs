using Newtonsoft.Json;

namespace MagicCrawler.Models
{
    public class Endpoint
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("tag")]
        public string Tag { get; set; }

        [JsonProperty("file")]
        public string File { get; set; }

        public string GetTag()
        {
            return !string.IsNullOrWhiteSpace(Tag) ? Tag : Url;
        }
    }
}
