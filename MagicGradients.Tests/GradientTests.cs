using FluentAssertions;
using FluentAssertions.Execution;
using System.Collections.Generic;
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
                Stops = new List<GradientStop>
                {
                    new GradientStop { Offset = 0.1f },
                    new GradientStop { Offset = 0.2f }
                }
            };

            // Act
            gradient.SetupUndefinedOffsets();

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
                Stops = new List<GradientStop>
                {
                    new GradientStop(),
                    new GradientStop(),
                    new GradientStop()
                }
            };
            
            // Act
            gradient.SetupUndefinedOffsets();

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
                Stops = new List<GradientStop>
                {
                    new GradientStop(),
                    new GradientStop { Offset = 0.1f },
                    new GradientStop(),
                    new GradientStop { Offset = 0.9f },
                    new GradientStop(),
                }
            };

            // Act
            gradient.SetupUndefinedOffsets();

            // Assert
            using (new AssertionScope())
            {
                gradient.Stops[0].Offset.Should().Be(0f);
                gradient.Stops[1].Offset.Should().Be(0.1f);
                gradient.Stops[2].Offset.Should().Be(0.5f);
                gradient.Stops[3].Offset.Should().Be(0.9f);
                gradient.Stops[4].Offset.Should().Be(1f);
            }
        }
    }
}
