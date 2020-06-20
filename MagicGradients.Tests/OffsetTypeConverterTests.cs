using FluentAssertions;
using FluentAssertions.Execution;
using System;
using System.Collections.Generic;
using Xunit;

namespace MagicGradients.Tests
{
    public class OffsetTypeConverterTests
    {
        public static IEnumerable<object[]> ValidOffsets => new List<object[]>
        {
            new object[] { "0.5", Offset.Prop(0.5) },
            new object[] { " 80%", Offset.Prop(0.8) },
            new object[] { "40px ", Offset.Abs(40) }
        };

        [Theory]
        [MemberData(nameof(ValidOffsets))]
        public void ConvertFromInvariantString_ValidValue_ValueConverted(string value, Offset expected)
        {
            // Arrange
            var converter = new OffsetTypeConverter();

            // Act
            var converted = converter.ConvertFromInvariantString(value);

            // Assert
            converted.Should().BeOfType<Offset>().And.BeEquivalentTo(expected);
        }

        [Theory]
        [InlineData("30sp")]
        [InlineData("15%%")]
        public void ConvertFromInvariantString_InvalidValue_ValueConverted(string value)
        {
            // Arrange
            var converter = new OffsetTypeConverter();

            // Act
            Action act = () => converter.ConvertFromInvariantString(value);

            // Assert
            act.Should().Throw<InvalidOperationException>();
        }

        [Theory]
        [InlineData("70%", true, 0.7)]
        [InlineData("20px", true, 20)]
        [InlineData("15ss", false, 0)]
        public void TryConvertOffset_GivenValue_ProperStatusAndNumber(string input, bool expectedSuccess, double expectedValue)
        {
            // Arrange
            var converter = new OffsetTypeConverter();

            // Act
            var success = converter.TryExtractOffset(input, out var result);

            // Assert
            using (new AssertionScope())
            {
                success.Should().Be(expectedSuccess);
                result.Value.Should().Be(expectedValue);
            }
        }

        [Theory]
        [InlineData("%", 0, false)]
        [InlineData("", 0, false)]
        [InlineData(null, 0, false)]
        [InlineData("10", 0, false)]
        [InlineData("0%", 0, true)]
        [InlineData("23%", 0.23, true)]
        [InlineData("101%", 1, true)]
        [InlineData("100%", 1, true)]
        public void TryConvertPercentToOffset_ConvertingToken_SuccessAndResultConvertedCorrectly(string token, double expectedResult, bool expectedSuccess)
        {
            // Arrange
            var converter = new OffsetTypeConverter();

            // Act
            var success = converter.TryExtractOffset(token, out var result);

            // Assert
            success.Should().Be(expectedSuccess);
            result.Value.Should().Be(expectedResult);
        }
    }
}
