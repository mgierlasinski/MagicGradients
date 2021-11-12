using MagicGradients;

namespace GradientsApp.Models
{
    public class CategoryItem
    {
        public string Name { get; set; }
        public IGradientSource Source { get; set; }
        public string Tag { get; set; }
    }
}
