using MagicGradients;

namespace Playground.Models
{
    public class GradientCategory
    {
        public string Name { get; set; }

        public string Tag { get; set; }

        public IGradientSource GradientSource { get; set; }
    }
}
