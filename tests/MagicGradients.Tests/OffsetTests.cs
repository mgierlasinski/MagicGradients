using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace MagicGradients.Tests
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
    }
}
