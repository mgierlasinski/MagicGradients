using System.Collections.Generic;

namespace MagicCrawler.Models
{
    public class Gradient
    {
        public string Slug { get; set; }
        public string Stylesheet { get; set; }
        public string Size { get; set; }
        public List<string> Tags { get; set; }
    }
}
