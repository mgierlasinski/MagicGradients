using FluentAssertions;
using System;
using Xamarin.Forms;
using Xunit;

namespace MagicGradients.Tests.Xaml
{
    public class TypeConverterTests<TValue, TConverter> : IClassFixture<TConverter> where TConverter : TypeConverter
    {
        protected TConverter Converter { get; }

        public TypeConverterTests(TConverter converter)
        {
            Converter = converter;
        }

        public void AssertValueIsExpected(string value, TValue expected)
        {
            // Act
            var converted = Converter.ConvertFromInvariantString(value);

            // Assert
            converted.Should().BeOfType<TValue>().And.BeEquivalentTo(expected);
        }

        public void AssertThrowsException(string value)
        {
            // Act
            Action act = () => Converter.ConvertFromInvariantString(value);

            // Assert
            act.Should().Throw<InvalidOperationException>();
        }
    }
}
