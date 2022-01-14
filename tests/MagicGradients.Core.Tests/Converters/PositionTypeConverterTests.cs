using MagicGradients.Converters;
using System.Collections.Generic;
using Xunit;

namespace MagicGradients.Core.Tests.Converters
{
    [Trait("Converter", "Position")]
    public class PositionTypeConverterTests : TypeConverterTests<Position, PositionTypeConverter>
    {
        public static IEnumerable<object[]> ValidValuesFrom => new List<object[]>
        {
            new object[] { null, Position.Zero },
            new object[] { "", Position.Zero },
            new object[] { " ", Position.Zero },
            new object[] { "80% 40%", Position.Prop(0.8, 0.4) },
            new object[] { "20%,30%", Position.Prop(0.2, 0.3) },
            new object[] { "40px 65px", Position.Abs(40, 65) },
            new object[] { "15px,70px", Position.Abs(15, 70) },
            new object[] { "90,200", Position.Abs(90, 200) },
            new object[] { "300, 500", Position.Abs(300, 500) },
            new object[] { "250 ,400", Position.Abs(250, 400) }
        };

        public static IEnumerable<object[]> ValidValuesTo => new List<object[]>
        {
            new object[] { Position.Zero, "0px 0px" },
            new object[] { Position.Abs(40, 65), "40px 65px" },
            new object[] { Position.Prop(0.8, 0.4), "80% 40%" }
        };

        public static IEnumerable<object[]> InvalidValues => new List<object[]>
        {
            new object[] { "30dp" },
            new object[] { "15em" }
        };

        [Theory]
        [MemberData(nameof(ValidValuesFrom))]
        public void ConvertFrom_ValidValue_ValueConverted(string value, Position expected)
        {
            // Assert
            Assert_ConvertFrom_IsExpected(value, expected);
        }

        [Theory]
        [MemberData(nameof(ValidValuesTo))]
        public void ConvertTo_ValidValue_IsExpected(Position value, string expected)
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

        public PositionTypeConverterTests(PositionTypeConverter converter) : base(converter) { }
    }
}
