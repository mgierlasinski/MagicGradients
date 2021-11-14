using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;
using static MagicGradients.FlagsHelper;
using static MagicGradients.RadialGradientFlags;

namespace MagicGradients.Tests
{
    [Trait("Feature", "Core")]
    public class FlagsHelperTests
    {
        [Theory]
        [InlineData(None, false)]
        [InlineData(All, true)]
        public void InitialTest(RadialGradientFlags initial, bool expected)
        {
            // Arrange
            var flags = initial;

            // Assert
            using (new AssertionScope())
            {
                IsSet(flags, XProportional).Should().Be(expected);
                IsSet(flags, YProportional).Should().Be(expected);
                IsSet(flags, PositionProportional).Should().Be(expected);
                IsSet(flags, WidthProportional).Should().Be(expected);
                IsSet(flags, HeightProportional).Should().Be(expected);
                IsSet(flags, SizeProportional).Should().Be(expected);
            }
        }

        [Fact]
        public void SetXProportional_SingleFlagSet()
        {
            // Arrange
            var flags = None;

            // Act
            Set(ref flags, XProportional);

            // Assert
            using (new AssertionScope())
            {
                IsSet(flags, XProportional).Should().Be(true);
                IsSet(flags, YProportional).Should().Be(false);
                IsSet(flags, PositionProportional).Should().Be(false);
            }
        }

        [Fact]
        public void SetXYProportional_AllPositionFlagsSet()
        {
            // Arrange
            var flags = None;

            // Act
            Set(ref flags, XProportional);
            Set(ref flags, YProportional);

            // Assert
            using (new AssertionScope())
            {
                IsSet(flags, XProportional).Should().Be(true);
                IsSet(flags, YProportional).Should().Be(true);
                IsSet(flags, PositionProportional).Should().Be(true);
            }
        }

        [Fact]
        public void SetPositionProportional_AllPositionFlagsSet()
        {
            // Arrange
            var flags = None;

            // Act
            Set(ref flags, PositionProportional);

            // Assert
            using (new AssertionScope())
            {
                IsSet(flags, XProportional).Should().Be(true);
                IsSet(flags, YProportional).Should().Be(true);
                IsSet(flags, PositionProportional).Should().Be(true);
            }
        }

        [Fact]
        public void UnsetXProportional_SingleFlagSet()
        {
            // Arrange
            var flags = PositionProportional;

            // Act
            Unset(ref flags, XProportional);

            // Assert
            using (new AssertionScope())
            {
                IsSet(flags, XProportional).Should().Be(false);
                IsSet(flags, YProportional).Should().Be(true);
                IsSet(flags, PositionProportional).Should().Be(false);
            }
        }

        [Fact]
        public void SetWidthProportional_SingleFlagSet()
        {
            // Arrange
            var flags = None;

            // Act
            Set(ref flags, WidthProportional);

            // Assert
            using (new AssertionScope())
            {
                IsSet(flags, WidthProportional).Should().Be(true);
                IsSet(flags, HeightProportional).Should().Be(false);
                IsSet(flags, SizeProportional).Should().Be(false);
            }
        }

        [Fact]
        public void SetWidthHeightProportional_AllSizeFlagsSet()
        {
            // Arrange
            var flags = None;

            // Act
            Set(ref flags, WidthProportional);
            Set(ref flags, HeightProportional);

            // Assert
            using (new AssertionScope())
            {
                IsSet(flags, WidthProportional).Should().Be(true);
                IsSet(flags, HeightProportional).Should().Be(true);
                IsSet(flags, SizeProportional).Should().Be(true);
            }
        }

        [Fact]
        public void SetSizeProportional_AllSizeFlagsSet()
        {
            // Arrange
            var flags = None;

            // Act
            Set(ref flags, SizeProportional);

            // Assert
            using (new AssertionScope())
            {
                IsSet(flags, WidthProportional).Should().Be(true);
                IsSet(flags, HeightProportional).Should().Be(true);
                IsSet(flags, SizeProportional).Should().Be(true);
            }
        }

        [Fact]
        public void UnsetWidthProportional_SingleFlagSet()
        {
            // Arrange
            var flags = SizeProportional;

            // Act
            Unset(ref flags, WidthProportional);

            // Assert
            using (new AssertionScope())
            {
                IsSet(flags, WidthProportional).Should().Be(false);
                IsSet(flags, HeightProportional).Should().Be(true);
                IsSet(flags, SizeProportional).Should().Be(false);
            }
        }
    }
}
