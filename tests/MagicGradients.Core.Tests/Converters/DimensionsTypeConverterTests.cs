using MagicGradients.Converters;
using System.Collections.Generic;
using Xunit;

namespace MagicGradients.Core.Tests.Converters
{
    [Trait("Feature", "Converters")]
    public class DimensionsTypeConverterTests : TypeConverterTests<Dimensions, DimensionsTypeConverter>
    {
        public static IEnumerable<object[]> ValidValuesFrom => new List<object[]>
        {
            new object[] { "80% 40%", Dimensions.Prop(0.8, 0.4) },
            new object[] { "20%,30%", Dimensions.Prop(0.2, 0.3) },
            new object[] { "40px 65px", Dimensions.Abs(40, 65) },
            new object[] { "15px,70px", Dimensions.Abs(15, 70) },
            new object[] { "90,200", Dimensions.Abs(90, 200) },
            new object[] { "300, 500", Dimensions.Abs(300, 500) },
            new object[] { "250 ,400", Dimensions.Abs(250, 400) }
        };

        public static IEnumerable<object[]> ValidValuesTo => new List<object[]>
        {
            new object[] { Dimensions.Zero, "0px 0px" },
            new object[] { Dimensions.Abs(40, 65), "40px 65px" },
            new object[] { Dimensions.Prop(0.8, 0.4), "80% 40%" }
        };

        public static IEnumerable<object[]> InvalidValues => new List<object[]>
        {
            new object[] { null },
            new object[] { "" },
            new object[] { " " },
            new object[] { "30dp" },
            new object[] { "15em" }
        };

        [Theory]
        [MemberData(nameof(ValidValuesFrom))]
        public void ConvertFrom_ValidValue_ValueConverted(string value, Dimensions expected)
        {
            // Assert
            Assert_ConvertFrom_IsExpected(value, expected);
        }

        [Theory]
        [MemberData(nameof(ValidValuesTo))]
        public void ConvertTo_ValidValue_IsExpected(Dimensions value, string expected)
        {
            // Assert
            Assert_ConvertTo_IsExpected(value, expected);
        }

        [Theory]
        [MemberData(nameof(InvalidValues))]
        public void ConvertFrom_InvalidValue_ThrowException(string value)
        {
            // Assert
            Assert_ConvertFrom_ThrowsException(value);
        }

        public DimensionsTypeConverterTests(DimensionsTypeConverter converter) : base(converter) { }
    }
}
