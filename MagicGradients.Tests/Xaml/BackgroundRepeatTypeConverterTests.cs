﻿using MagicGradients.Xaml;
using System.Collections.Generic;
using Xunit;

namespace MagicGradients.Tests.Xaml
{
    public class BackgroundRepeatTypeConverterTests : TypeConverterTests<BackgroundRepeat, BackgroundRepeatTypeConverter>
    {
        public static IEnumerable<object[]> ValidValues => new List<object[]>
        {
            new object[] { "repeat", BackgroundRepeat.Repeat },
            new object[] { "Repeat", BackgroundRepeat.Repeat },
            new object[] { "repeat-x", BackgroundRepeat.RepeatX },
            new object[] { "RepeatX", BackgroundRepeat.RepeatX },
            new object[] { "repeat-y", BackgroundRepeat.RepeatY },
            new object[] { "RepeatY", BackgroundRepeat.RepeatY },
            new object[] { "no-repeat", BackgroundRepeat.NoRepeat },
            new object[] { "NoRepeat", BackgroundRepeat.NoRepeat }
        };

        public static IEnumerable<object[]> InvalidValues => new List<object[]>
        {
            new object[] { null },
            new object[] { "", },
            new object[] { " " },
            new object[] { "repea" },
            new object[] { "repeat-xy" }
        };

        [Theory]
        [MemberData(nameof(ValidValues))]
        public void ConvertFromInvariantString_ValidValue_ValueConverted(string value, BackgroundRepeat expected)
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

        public BackgroundRepeatTypeConverterTests(BackgroundRepeatTypeConverter converter) : base(converter) { }
    }
}
