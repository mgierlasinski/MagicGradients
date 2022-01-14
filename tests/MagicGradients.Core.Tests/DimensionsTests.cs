using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace MagicGradients.Core.Tests
{
    [Trait("Primitive", "Dimensions")]
    public class DimensionsTests
    {
        public static IEnumerable<object[]> IsZeroData => new List<object[]>
        {
            new object[] { Dimensions.Zero, true },
            new object[] { Dimensions.Abs(0, 0), true },
            new object[] { Dimensions.Prop(0, 0), true },
            new object[] { Dimensions.Abs(20, 0), false },
            new object[] { Dimensions.Prop(0, 20), false }
        };

        public static IEnumerable<object[]> NotEqualData => new List<object[]>
        {
            new object[] { Dimensions.Abs(30, 40), Dimensions.Abs(40, 30) },
            new object[] { Dimensions.Prop(0.8, 0.3), Dimensions.Prop(0.9, 0.3) },
            new object[] { Dimensions.Abs(1, 1), Dimensions.Prop(1, 1) }
        };

        [Theory]
        [MemberData(nameof(IsZeroData))]
        public void IsZero_HasExpectedValue(Dimensions value, bool isZero)
        {
            // Assert
            value.IsZero.Should().Be(isZero);
        }

        [Fact]
        public void Equals_SameValues_IsTrue()
        {
            // Arrange
            var value1 = Dimensions.Abs(100, 100);
            var value2 = Dimensions.Abs(100, 100);

            // Act
            var result = value1.Equals(value2);

            // Assert
            result.Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(NotEqualData))]
        public void Equals_DifferentValues_IsFalse(Dimensions value1, Dimensions value2)
        {
            // Arrange & Act
            var result = value1.Equals(value2);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void OperatorEquals_SameValues_IsTrue()
        {
            // Arrange
            var value1 = Dimensions.Abs(100, 100);
            var value2 = Dimensions.Abs(100, 100);

            // Act
            var result = value1 == value2;

            // Assert
            result.Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(NotEqualData))]
        public void OperatorEquals_DifferentValues_IsFalse(Dimensions value1, Dimensions value2)
        {
            // Arrange & Act
            var result = value1 == value2;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void OperatorNotEqual_SameValues_IsFalse()
        {
            // Arrange
            var value1 = Dimensions.Abs(100, 100);
            var value2 = Dimensions.Abs(100, 100);

            // Act
            var result = value1 != value2;

            // Assert
            result.Should().BeFalse();
        }

        [Theory]
        [MemberData(nameof(NotEqualData))]
        public void OperatorNotEqual_DifferentValues_IsTrue(Dimensions value1, Dimensions value2)
        {
            // Arrange & Act
            var result = value1 != value2;

            // Assert
            result.Should().BeTrue();
        }
    }
}
