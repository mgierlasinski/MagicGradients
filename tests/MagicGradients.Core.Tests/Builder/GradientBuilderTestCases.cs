using MagicGradients.Builder;
using Xunit;

namespace MagicGradients.Core.Tests.Builder
{
    public class GradientBuilderTestCases : TheoryData<GradientBuilderTestCase>
    {
        public GradientBuilderTestCases()
        {
            Add(new LinearTestCase());
            Add(new RadialTestCase());
        }
    }

    public abstract class GradientBuilderTestCase
    {
        public abstract void AddGradientTo(GradientBuilder builder);
    }

    public class LinearTestCase : GradientBuilderTestCase
    {
        public override void AddGradientTo(GradientBuilder builder)
        {
            builder.AddLinearGradient(g => g.Rotate(45));
        }
    }

    public class RadialTestCase : GradientBuilderTestCase
    {
        public override void AddGradientTo(GradientBuilder builder)
        {
            builder.AddRadialGradient(g => g
                .Circle().At(0.5, 0.5)
                .StretchTo(RadialGradientSize.ClosestSide));
        }
    }
}
