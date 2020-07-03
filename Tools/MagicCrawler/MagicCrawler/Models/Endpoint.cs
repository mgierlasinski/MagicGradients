using Newtonsoft.Json;

namespace MagicCrawler.Models
{
    public class Endpoint
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("tag")]
        public string Tag { get; set; }

        [JsonProperty("file")]
        public string File { get; set; }

        [JsonProperty("isVirtual")]
        public bool IsVirtual { get; set; }

        public string GetUrl()
        {
            return !string.IsNullOrWhiteSpace(Url) ? Url : Id.ToLower();
        }

        public string GetTag()
        {
            return !string.IsNullOrWhiteSpace(Tag) ? Tag : Id.ToLower();
        }

        public string GetFile()
        {
            return !string.IsNullOrWhiteSpace(File) ? File : $"{Id}.json";
        }
    }
}
