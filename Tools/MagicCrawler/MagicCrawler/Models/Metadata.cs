using Newtonsoft.Json;
using System;

namespace MagicCrawler.Models
{
    public class Metadata
    {
        [JsonProperty("version")]
        public ushort Version { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("namespace")]
        public string NameSpace { get; set; }

        [JsonProperty("categories")]
        public string Categories { get; set; }

        [JsonProperty("themes")]
        public string Themes { get; set; }

        [JsonProperty("gradients")]
        public string[] Gradients { get; set; }
    }
}
