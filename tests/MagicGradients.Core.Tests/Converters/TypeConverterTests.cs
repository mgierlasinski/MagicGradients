using FluentAssertions;
using System;
using System.ComponentModel;
using Xunit;

namespace MagicGradients.Core.Tests.Converters
{
    [Trait("Feature", "Converters")]
    public class TypeConverterTests<TValue, TConverter> : IClassFixture<TConverter> where TConverter : TypeConverter
    {
        protected TConverter Converter { get; }

        public TypeConverterTests(TConverter converter)
        {
            Converter = converter;
        }

        public void Assert_ConvertFrom_IsExpected(string value, TValue expected)
        {
            // Act
            var converted = Converter.ConvertFromInvariantString(value);

            // Assert
            converted.Should().BeOfType<TValue>().And.BeEquivalentTo(expected);
        }

        public void Assert_ConvertTo_IsExpected(TValue value, string expected)
        {
            // Act
            var converted = Converter.ConvertToInvariantString(value);

            // Assert
            converted.Should().Be(expected);
        }

        public void Assert_ConvertFrom_ThrowsException(string value)
        {
            // Act
            Action act = () => Converter.ConvertFromInvariantString(value);

            // Assert
            act.Should().Throw<InvalidOperationException>().WithMessage($"Cannot convert \"{value}\" into *");
        }
    }
}
