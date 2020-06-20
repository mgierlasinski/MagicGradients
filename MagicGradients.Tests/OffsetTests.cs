using FluentAssertions;
using Xunit;

namespace MagicGradients.Tests
{
    public class OffsetTests
    {
        [Theory]
        [InlineData(20, false)]
        [InlineData(0.4, false)]
        [InlineData(0, false)]
        [InlineData(-1, true)]
        [InlineData(-10, true)]
        public void ValueSet_IsEmpty_HasExpectedValue(double value, bool isEmpty)
        {
            // Arrange & Act
            var offset = new Offset(value, OffsetType.Absolute);

            // Assert
            offset.IsEmpty.Should().Be(isEmpty);
        }
    }
}
