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
                    new GradientStop { Offset = 0.1f },
                    new GradientStop { Offset = 0.2f }
                }
            };

            // Act
            gradient.Measure(0, 0);

            // Assert
            using (new AssertionScope())
            {
                gradient.Stops[0].Offset.Should().Be(0.1f);
                gradient.Stops[1].Offset.Should().Be(0.2f);
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
                gradient.Stops[0].Offset.Should().Be(0f);
                gradient.Stops[1].Offset.Should().Be(0.5f);
                gradient.Stops[2].Offset.Should().Be(1f);
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
                    new GradientStop { Offset = 0.6f },
                    new GradientStop(),
                    new GradientStop(),
                    new GradientStop { Offset = 0.9f },
                    new GradientStop()
                }
            };

            // Act
            gradient.Measure(0, 0);

            // Assert
            using (new AssertionScope())
            {
                gradient.Stops[0].Offset.Should().Be(0f);
                gradient.Stops[1].Offset.Should().BeInRange(0.19f, 0.21f);
                gradient.Stops[2].Offset.Should().BeInRange(0.39f, 0.41f);
                gradient.Stops[3].Offset.Should().Be(0.6f);
                gradient.Stops[4].Offset.Should().BeInRange(0.69f, 0.71f);
                gradient.Stops[5].Offset.Should().BeInRange(0.79f, 0.81f);
                gradient.Stops[6].Offset.Should().Be(0.9f);
                gradient.Stops[7].Offset.Should().Be(1f);
            }
        }
    }
}
