using FluentAssertions;
using FluentAssertions.Execution;
using Xamarin.Forms;
using Xunit;

namespace MagicGradients.Tests
{
    public class GradientBuilderTests
    {
        [Theory]
        [ClassData(typeof(GradientBuilderTestCases))]
        public void AddGradient_AndStops_SingleGradientWithStops(GradientBuilderTestCase testCase)
        {
            // Arrange
            var builder = new GradientBuilder();

            // Act
            testCase.AddGradient(builder);
            builder.AddStop(Color.White);
            builder.AddStop(Color.Black);

            var gradients = builder.Build();

            // Assert
            using (new AssertionScope())
            {
                gradients.Should().HaveCount(1);
                gradients[0].Stops.Should().HaveCount(2);
            }
        }

        [Theory]
        [ClassData(typeof(GradientBuilderTestCases))]
        public void AddStops_AndGradient_AndStops_TwoGradientsWithStops(GradientBuilderTestCase testCase)
        {
            // Arrange
            var builder = new GradientBuilder();

            // Act
            builder.AddStop(Color.Red);
            testCase.AddGradient(builder);
            builder.AddStop(Color.White);
            builder.AddStop(Color.Black);

            var gradients = builder.Build();

            // Assert
            using (new AssertionScope())
            {
                gradients.Should().HaveCount(2);
                gradients[0].Stops.Should().HaveCount(1);
                gradients[1].Stops.Should().HaveCount(2);
            }
        }

        [Fact]
        public void AddOnlyStops_DefaultGradientWithStops()
        {
            // Arrange
            var builder = new GradientBuilder();

            // Act
            builder.AddStop(Color.White);
            builder.AddStop(Color.Black);

            var gradients = builder.Build();

            // Assert
            using (new AssertionScope())
            {
                gradients.Should().HaveCount(1);
                gradients[0].Should().BeOfType<LinearGradient>();
                gradients[0].Stops.Should().HaveCount(2);
            }
        }
    }
}
