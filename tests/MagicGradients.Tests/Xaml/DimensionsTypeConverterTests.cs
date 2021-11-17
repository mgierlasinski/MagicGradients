using MagicGradients.Xaml;
using System.Collections.Generic;
using Xunit;

namespace MagicGradients.Tests.Xaml
{
    [Trait("Feature", "Xaml")]
    public class DimensionsTypeConverterTests : TypeConverterTests<Dimensions, DimensionsTypeConverter>
    {
        public static IEnumerable<object[]> ValidValues => new List<object[]>
        {
            new object[] { null, Dimensions.Zero },
            new object[] { "", Dimensions.Zero },
            new object[] { " ", Dimensions.Zero },
            new object[] { "80% 40%", Dimensions.Prop(0.8, 0.4) },
            new object[] { "20%,30%", Dimensions.Prop(0.2, 0.3) },
            new object[] { "40px 65px", Dimensions.Abs(40, 65) },
            new object[] { "15px,70px", Dimensions.Abs(15, 70) },
            new object[] { "90,200", Dimensions.Abs(90, 200) },
            new object[] { "300, 500", Dimensions.Abs(300, 500) },
            new object[] { "250 ,400", Dimensions.Abs(250, 400) }
        };

        public static IEnumerable<object[]> InvalidValues => new List<object[]>
        {
            new object[] { "30dp" },
            new object[] { "15em" },
        };

        [Theory]
        [MemberData(nameof(ValidValues))]
        public void ConvertFromInvariantString_ValidValue_ValueConverted(string value, Dimensions expected)
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

        public DimensionsTypeConverterTests(DimensionsTypeConverter converter) : base(converter) { }
    }
}
