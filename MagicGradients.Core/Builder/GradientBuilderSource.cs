using System.Collections.Generic;
using System.Linq;

namespace MagicGradients.Builder
{
    public class GradientBuilderSource : IGradientSource
    {
        private readonly List<IGradient> _gradients;

        public GradientBuilderSource(GradientBuilder builder)
        {
            _gradients = builder.Build().ToList();
        }

        public IReadOnlyList<IGradient> GetGradients()
        {
            return _gradients;
        }
    }

    public static class GradientBuilderExtensions
    {
        public static IGradientSource ToSource(this GradientBuilder builder)
        {
            return new GradientBuilderSource(builder);
        }
    }
}
