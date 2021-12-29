using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace MagicGradients.Core.Tests
{
    [Trait("Feature", "Core")]
    public class OffsetTests
    {
        public static IEnumerable<object[]> TestCases => new List<object[]>
        {
            new object[] { Offset.Empty, true },
            new object[] { Offset.Zero, false },
            new object[] { Offset.Abs(20), false },
            new object[] { Offset.Prop(0.4), false },
            new object[] { Offset.Prop(-0.5), true },
            new object[] { Offset.Prop(-1), true },
            new object[] { Offset.Abs(-10), true }
        };

        [Theory]
        [MemberData(nameof(TestCases))]
        public void ValueSet_IsEmpty_HasExpectedValue(Offset offset, bool isEmpty)
        {
            // Assert
            offset.IsEmpty.Should().Be(isEmpty);
        }
        
        [Theory]
        [InlineData("%", 0, false)]
        [InlineData("", 0, false)]
        [InlineData(null, 0, false)]
        [InlineData("10", 0, false)]
        [InlineData("15ss", 0, false)]
        [InlineData("0%", 0, true)]
        [InlineData("23%", 0.23, true)]
        [InlineData("20px", 20, true)]
        [InlineData("70%", 0.7, true)]
        [InlineData("101%", 1, true)]
        [InlineData("100%", 1, true)]
        public void TryParseWithUnit_ConvertingToken_ProperValueAndStatus(string token, double expectedResult, bool expectedSuccess)
        {
            // Act
            var success = Offset.TryParseWithUnit(token, out var result);

            // Assert
            success.Should().Be(expectedSuccess);
            result.Value.Should().Be(expectedResult);
        }
    }
}
