using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace MagicGradients.Core.Tests
{
    [Trait("Primitive", "Dimensions")]
    public class DimensionsTests
    {
        public static IEnumerable<object[]> TestCases => new List<object[]>
        {
            new object[] { Dimensions.Zero, true },
            new object[] { Dimensions.Abs(0, 0), true },
            new object[] { Dimensions.Prop(0, 0), true },
            new object[] { Dimensions.Abs(20, 0), false },
            new object[] { Dimensions.Prop(0, 20), false }
        };

        [Theory]
        [MemberData(nameof(TestCases))]
        public void ValueSet_IsZero_HasExpectedValue(Dimensions value, bool isZero)
        {
            // Assert
            value.IsZero.Should().Be(isZero);
        }
    }
}
