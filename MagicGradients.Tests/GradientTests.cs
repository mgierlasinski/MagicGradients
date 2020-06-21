using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace MagicGradients.Tests
{
    public class GradientTests
    {
        [Fact]
        public void SetupUndefinedOffsets_HasDefinedOffsets_NothingChanged()
        {
            // Arrange
            var gradient = new LinearGradient
            {
                Stops = new GradientElements<GradientStop>
                {
                    new GradientStop { Offset = new Offset(0.1f, OffsetType.Proportional) },
                    new GradientStop { Offset = new Offset(0.2f, OffsetType.Proportional) }
                }
            };

            // Act
            gradient.Measure(0, 0);

            // Assert
            using (new AssertionScope())
            {
                gradient.Stops[0].RenderOffset.Should().Be(0.1f);
                gradient.Stops[1].RenderOffset.Should().Be(0.2f);
            }
        }

        [Fact]
        public void SetupUndefinedOffsets_HasUndefinedOffsets_AutomaticallySetUp()
        {
            // Arrange
            var gradient = new LinearGradient
            {
                Stops = new GradientElements<GradientStop>
                {
                    new GradientStop(),
                    new GradientStop(),
                    new GradientStop()
                }
            };
            
            // Act
            gradient.Measure(0, 0);

            // Assert
            using (new AssertionScope())
            {
                gradient.Stops[0].RenderOffset.Should().Be(0f);
                gradient.Stops[1].RenderOffset.Should().Be(0.5f);
                gradient.Stops[2].RenderOffset.Should().Be(1f);
            }
        }

        [Fact]
        public void SetupUndefinedOffsets_HasMixedOffsets_OnlySetUpUndefined()
        {
            // Arrange
            var gradient = new LinearGradient
            {
                Stops = new GradientElements<GradientStop>
                {
                    new GradientStop(),
                    new GradientStop(),
                    new GradientStop(),
                    new GradientStop { Offset = new Offset(0.6f, OffsetType.Proportional) },
                    new GradientStop(),
                    new GradientStop(),
                    new GradientStop { Offset = new Offset(0.9f, OffsetType.Proportional) },
                    new GradientStop()
                }
            };

            // Act
            gradient.Measure(0, 0);

            // Assert
            using (new AssertionScope())
            {
                gradient.Stops[0].RenderOffset.Should().Be(0f);
                gradient.Stops[1].RenderOffset.Should().BeInRange(0.19f, 0.21f);
                gradient.Stops[2].RenderOffset.Should().BeInRange(0.39f, 0.41f);
                gradient.Stops[3].RenderOffset.Should().Be(0.6f);
                gradient.Stops[4].RenderOffset.Should().BeInRange(0.69f, 0.71f);
                gradient.Stops[5].RenderOffset.Should().BeInRange(0.79f, 0.81f);
                gradient.Stops[6].RenderOffset.Should().Be(0.9f);
                gradient.Stops[7].RenderOffset.Should().Be(1f);
            }
        }
    }
}
