using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace MagicGradients.Core.Tests
{
    [Trait("Primitive", "Position")]
    public class PositionTests
    {
        public static IEnumerable<object[]> IsZeroData => new List<object[]>
        {
            new object[] { Position.Zero, true },
            new object[] { Position.Abs(0, 0), true },
            new object[] { Position.Prop(0, 0), true },
            new object[] { Position.Abs(20, 0), false },
            new object[] { Position.Prop(0, 20), false }
        };

        public static IEnumerable<object[]> NotEqualData => new List<object[]>
        {
            new object[] { Position.Abs(30, 40), Position.Abs(40, 30) },
            new object[] { Position.Prop(0.8, 0.3), Position.Prop(0.9, 0.3) },
            new object[] { Position.Abs(1, 1), Position.Prop(1, 1) }
        };

        [Theory]
        [MemberData(nameof(IsZeroData))]
        public void IsZero_HasExpectedValue(Position value, bool isZero)
        {
            // Assert
            value.IsZero.Should().Be(isZero);
        }

        [Fact]
        public void Equals_SameValues_IsTrue()
        {
            // Arrange
            var value1 = Position.Abs(100, 100);
            var value2 = Position.Abs(100, 100);

            // Act
            var result = value1.Equals(value2);

            // Assert
            result.Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(NotEqualData))]
        public void Equals_DifferentValues_IsFalse(Position value1, Position value2)
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
            var value1 = Position.Abs(100, 100);
            var value2 = Position.Abs(100, 100);

            // Act
            var result = value1 == value2;

            // Assert
            result.Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(NotEqualData))]
        public void OperatorEquals_DifferentValues_IsFalse(Position value1, Position value2)
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
            var value1 = Position.Abs(100, 100);
            var value2 = Position.Abs(100, 100);

            // Act
            var result = value1 != value2;

            // Assert
            result.Should().BeFalse();
        }

        [Theory]
        [MemberData(nameof(NotEqualData))]
        public void OperatorNotEqual_DifferentValues_IsTrue(Position value1, Position value2)
        {
            // Arrange & Act
            var result = value1 != value2;

            // Assert
            result.Should().BeTrue();
        }
    }
}
