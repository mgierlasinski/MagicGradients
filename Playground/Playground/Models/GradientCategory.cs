using MagicGradients;

namespace Playground.Models
{
    public class GradientCategory
    {
        public string Name { get; set; }

        public string Tag => Name.ToLowerInvariant();

        public IGradientSource GradientSource { get; set; }

        public GradientCategory(string name)
        {
            Name = name;
        }
    }
}
