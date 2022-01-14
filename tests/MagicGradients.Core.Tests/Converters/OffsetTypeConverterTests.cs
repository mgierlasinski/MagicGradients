using MagicGradients.Converters;
using System.Collections.Generic;
using Xunit;

namespace MagicGradients.Core.Tests.Converters
{
    [Trait("Converter", "Offset")]
    public class OffsetTypeConverterTests : TypeConverterTests<Offset, OffsetTypeConverter>
    {
        public static IEnumerable<object[]> ValidValuesFrom => new List<object[]>
        {
            new object[] { "-1", Offset.Empty },
            new object[] { "0", Offset.Zero },
            new object[] { "0.5", Offset.Prop(0.5) },
            new object[] { " 80%", Offset.Prop(0.8) },
            new object[] { "40px ", Offset.Abs(40) }
        };

        public static IEnumerable<object[]> ValidValuesTo => new List<object[]>
        {
            new object[] { Offset.Empty, "" },
            new object[] { Offset.Zero, "0%" },
            new object[] { Offset.Prop(0.5), "50%" },
            new object[] { Offset.Abs(40), "40px" }
        };

        public static IEnumerable<object[]> InvalidValues => new List<object[]>
        {
            new object[] { null },
            new object[] { "" },
            new object[] { " " },
            new object[] { "30sp" },
            new object[] { "15%%" }
        };

        [Theory]
        [MemberData(nameof(ValidValuesFrom))]
        public void ConvertFrom_ValidValue_IsExpected(string value, Offset expected)
        {
            // Assert
            Assert_ConvertFrom_IsExpected(value, expected);
        }

        [Theory]
        [MemberData(nameof(ValidValuesTo))]
        public void ConvertTo_ValidValue_IsExpected(Offset value, string expected)
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
        
        public OffsetTypeConverterTests(OffsetTypeConverter converter) : base(converter) { }
    }
}
