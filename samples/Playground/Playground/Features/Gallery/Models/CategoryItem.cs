using MagicGradients;

namespace Playground.Features.Gallery.Models
{
    public class CategoryItem
    {
        public string Name { get; set; }
        public string Tag { get; set; }
        public int Count { get; set; }
        public IGradientSource GradientSource { get; set; }
    }
}
