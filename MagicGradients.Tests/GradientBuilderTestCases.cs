using Xamarin.Forms;
using Xunit;

namespace MagicGradients.Tests
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
        public abstract void AddGradient(GradientBuilder builder);
    }

    public class LinearTestCase : GradientBuilderTestCase
    {
        public override void AddGradient(GradientBuilder builder)
        {
            builder.AddLinearGradient(45);
        }
    }

    public class RadialTestCase : GradientBuilderTestCase
    {
        public override void AddGradient(GradientBuilder builder)
        {
            builder.AddRadialGradient(
                new Point(0.5, 0.5),
                RadialGradientShape.Circle,
                RadialGradientSize.ClosestSide);
        }
    }
}
