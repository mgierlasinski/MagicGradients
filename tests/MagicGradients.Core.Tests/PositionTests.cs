using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace MagicGradients.Core.Tests
{
    [Trait("Primitive", "Position")]
    public class PositionTests
    {
        public static IEnumerable<object[]> TestCases => new List<object[]>
        {
            new object[] { Position.Zero, true },
            new object[] { Position.Abs(0, 0), true },
            new object[] { Position.Prop(0, 0), true },
            new object[] { Position.Abs(20, 0), false },
            new object[] { Position.Prop(0, 20), false }
        };

        [Theory]
        [MemberData(nameof(TestCases))]
        public void ValueSet_IsZero_HasExpectedValue(Position value, bool isZero)
        {
            // Assert
            value.IsZero.Should().Be(isZero);
        }
    }
}
