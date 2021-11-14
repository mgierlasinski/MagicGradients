using MagicGradients.Animation;
using MagicGradients.Tests.Xaml;
using System.Collections.Generic;
using Xunit;

namespace MagicGradients.Tests.Animation
{
    [Trait("Feature", "Animation")]
    public class RepeatBehaviorTypeConverterTests : TypeConverterTests<RepeatBehavior, RepeatBehaviorTypeConverter>
    {
        public static IEnumerable<object[]> ValidValues => new List<object[]>
        {
            new object[] { "2x", new RepeatBehavior(RepeatBehaviorType.Count, 2)  },
            new object[] { "5 x", new RepeatBehavior(RepeatBehaviorType.Count, 5)  },
            new object[] { "Forever", new RepeatBehavior(RepeatBehaviorType.Forever, 0)  },
            new object[] { "forever", new RepeatBehavior(RepeatBehaviorType.Forever, 0)  }
        };

        public static IEnumerable<object[]> InvalidValues => new List<object[]>
        {
            new object[] { null },
            new object[] { "" },
            new object[] { " " },
            new object[] { "5" },
            new object[] { "Once" }
        };

        [Theory]
        [MemberData(nameof(ValidValues))]
        public void ConvertFromInvariantString_ValidValue_ValueConverted(string value, RepeatBehavior expected)
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

        public RepeatBehaviorTypeConverterTests(RepeatBehaviorTypeConverter converter) : base(converter) { }
    }
}
