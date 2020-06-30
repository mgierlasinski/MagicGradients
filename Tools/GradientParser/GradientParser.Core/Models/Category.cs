using Newtonsoft.Json;

namespace GradientParser.Core.Models
{
    public class Category
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("tag")]
        public string Tag { get; set; }

        [JsonProperty("output")]
        public string Output { get; set; }
    }
}
