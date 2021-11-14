using FluentAssertions;
using FluentAssertions.Execution;
using MagicGradients.Xaml;
using System.Collections.Generic;
using Xunit;

namespace MagicGradients.Tests.Xaml
{
    [Trait("Feature", "Xaml")]
    public class OffsetTypeConverterTests : TypeConverterTests<Offset, OffsetTypeConverter>
    {
        public static IEnumerable<object[]> ValidValues => new List<object[]>
        {
            new object[] { null, Offset.Empty },
            new object[] { "", Offset.Empty },
            new object[] { " ", Offset.Empty },
            new object[] { "-1", Offset.Empty },
            new object[] { "0", Offset.Zero },
            new object[] { "0.5", Offset.Prop(0.5) },
            new object[] { " 80%", Offset.Prop(0.8) },
            new object[] { "40px ", Offset.Abs(40) }
        };

        public static IEnumerable<object[]> InvalidValues => new List<object[]>
        {
            new object[] { "30sp" },
            new object[] { "15%%" }
        };

        [Theory]
        [MemberData(nameof(ValidValues))]
        public void ConvertFromInvariantString_ValidValue_ValueConverted(string value, Offset expected)
        {
            // Assert
            AssertValueIsExpected(value, expected);
        }

        [Theory]
        [MemberData(nameof(InvalidValues))]
        public void ConvertFromInvariantString_InvalidValue_ThrowException(string value)
        {
            // Assert
            AssertThrowsException(value);
        }

        [Theory]
        [InlineData("70%", true, 0.7)]
        [InlineData("20px", true, 20)]
        [InlineData("15ss", false, 0)]
        public void TryConvertOffset_GivenValue_ProperStatusAndNumber(string input, bool expectedSuccess, double expectedValue)
        {
            // Act
            var success = Converter.TryExtractOffset(input, out var result);

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
            // Act
            var success = Converter.TryExtractOffset(token, out var result);

            // Assert
            success.Should().Be(expectedSuccess);
            result.Value.Should().Be(expectedResult);
        }

        public OffsetTypeConverterTests(OffsetTypeConverter converter) : base(converter) { }
    }
}
