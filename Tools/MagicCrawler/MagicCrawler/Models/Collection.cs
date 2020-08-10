using System.Collections.Generic;

namespace MagicCrawler.Models
{
    public class Collection
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Stylesheet { get; set; }
        public int DisplayOrder { get; set; }
        public string BaseUrl { get; set; }
        public string Url { get; set; }
        public string Tag { get; set; }
        public string File { get; set; }
        public List<Gradient> Gradients { get; set; }

        public string GetFullUrl()
        {
            var url = !string.IsNullOrWhiteSpace(Url) ? Url : Id.ToLower();
            return $"{BaseUrl}{url}";
        }

        public string GetTag()
        {
            return !string.IsNullOrWhiteSpace(Tag) ? Tag : Id.ToLower();
        }

        public string GetFile()
        {
            return !string.IsNullOrWhiteSpace(File) ? File : $"{Id}.json";
        }

        public Category ToCategory()
        {
            return new Category
            {
                Name = Title,
                Tag = GetTag(),
                Stylesheet = Stylesheet,
                DisplayOrder = DisplayOrder,
                File = GetFile()
            };
        }
    }
}
