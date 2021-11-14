using System;
using FluentAssertions;
using FluentAssertions.Execution;
using MagicGradients.Builder;
using Xamarin.Forms;
using Xunit;

namespace MagicGradients.Tests.Builder
{
    [Trait("Feature", "Builder")]
    public class GradientBuilderTests
    {
        [Theory]
        [ClassData(typeof(GradientBuilderTestCases))]
        public void AddGradient_AndStops_SingleGradientWithStops(GradientBuilderTestCase testCase)
        {
            // Arrange
            var builder = new GradientBuilder();

            // Act
            testCase.AddGradientTo(builder);
            builder.AddStop(Color.White);
            builder.AddStop(Color.Black);

            var gradients = builder.Build();

            // Assert
            using (new AssertionScope())
            {
                gradients.Should().HaveCount(1);
                gradients[0].Stops.Should().HaveCount(2);
            }
        }

        [Theory]
        [ClassData(typeof(GradientBuilderTestCases))]
        public void AddStops_AndGradient_AndStops_TwoGradientsWithStops(GradientBuilderTestCase testCase)
        {
            // Arrange
            var builder = new GradientBuilder();

            // Act
            builder.AddStop(Color.Red);
            testCase.AddGradientTo(builder);
            builder.AddStop(Color.White);
            builder.AddStop(Color.Black);

            var gradients = builder.Build();

            // Assert
            using (new AssertionScope())
            {
                gradients.Should().HaveCount(2);
                gradients[0].Stops.Should().HaveCount(1);
                gradients[1].Stops.Should().HaveCount(2);
            }
        }

        [Fact]
        public void AddOnlyStops_DefaultGradientWithStops()
        {
            // Arrange
            var builder = new GradientBuilder();

            // Act
            builder.AddStop(Color.White);
            builder.AddStop(Color.Black);

            var gradients = builder.Build();

            // Assert
            using (new AssertionScope())
            {
                gradients.Should().HaveCount(1);
                gradients[0].Should().BeOfType<LinearGradient>();
                gradients[0].Stops.Should().HaveCount(2);
            }
        }

        [Fact]
        public void FluentApi_Build_CreateGradients()
        {
            // Act
            var gradients = new GradientBuilder()
                .AddLinearGradient(g => g
                    .Rotate(30)
                    .Repeat())
                .AddLinearGradient(g => g
                    .Rotate(20)
                    .Repeat()
                    .AddStop(Color.Red, Offset.Prop(0.2))
                    .AddStop(Color.Blue, Offset.Prop(0.4)))
                .AddRadialGradient(g => g
                    .Circle().At(0.5, 0.5, o => o.Proportional())
                    .Radius(200, 200, o => o.Absolute())
                    .StretchTo(RadialGradientSize.FarthestSide)
                    .Repeat()
                    .AddStops(Color.Red, Color.Green, Color.Blue))
                .Build();

            // Assert
            gradients.Should().HaveCount(3);
        }

        [Fact]
        public void FluentApi_BuildLinearNoOptions_ProperDefaultValues()
        {
            // Act
            var gradients = new GradientBuilder()
                .AddLinearGradient()
                .Build();

            // Assert
            using (new AssertionScope())
            {
                gradients.Should().HaveCount(1);
                gradients[0].Should().BeAssignableTo<LinearGradient>();

                var linear = (LinearGradient)gradients[0];
                linear.Angle.Should().Be(0);
                linear.IsRepeating.Should().BeFalse();
            }
        }

        [Fact]
        public void FluentApi_BuildRadialNoOptions_ProperDefaultValues()
        {
            // Act
            var gradients = new GradientBuilder()
                .AddRadialGradient()
                .Build();

            // Assert
            using (new AssertionScope())
            {
                gradients.Should().HaveCount(1);
                gradients[0].Should().BeAssignableTo<RadialGradient>();

                var radial = (RadialGradient)gradients[0];
                radial.Center.Should().BeEquivalentTo(new Point(0.5, 0.5));
                radial.RadiusX.Should().Be(-1);
                radial.RadiusY.Should().Be(-1);
                radial.Shape.Should().Be(RadialGradientShape.Ellipse);
                radial.Size.Should().Be(RadialGradientSize.FarthestCorner);
                radial.Flags.Should().Be(RadialGradientFlags.PositionProportional);
                radial.IsRepeating.Should().BeFalse();
            }
        }

        [Fact]
        public void FluentApi_ToSource_GradientCollectionWithGradients()
        {
            // Act
            var source = new GradientBuilder()
                .AddLinearGradient(g => g
                    .Rotate(20)
                    .AddStops(Color.Red, Color.Green, Color.Blue))
                .ToSource();

            // Assert
            using (new AssertionScope())
            {
                source.Should().BeOfType<GradientCollection>();
                source.GetGradients().Should().HaveCount(1);
            }
        }

        [Fact]
        public void AddCssGradient_ValidCss_GradientParsed()
        {
            // Act
            var gradients = new GradientBuilder()
                .AddCssGradient("linear-gradient(red, orange)")
                .Build();

            // Assert
            using (new AssertionScope())
            {
                gradients.Should().HaveCount(1);
                gradients[0].Should().BeOfType<LinearGradient>();
                gradients[0].Stops.Should().HaveCount(2);
                gradients[0].Stops[0].Color.Should().Be(Color.Red);
                gradients[0].Stops[1].Color.Should().Be(Color.Orange);
            }
        }

        [Theory]
        [InlineData("")]
        [InlineData("linear-gradient(black,white), radial-gradient(pink,brown)")]
        public void AddCssGradient_InvalidCss_InvalidOperationException(string css)
        {
            // Arrange
            var builder = new GradientBuilder().AddCssGradient(css);

            // Act
            Action action = () => builder.Build();

            // Assert
            action.Should().Throw<InvalidOperationException>();
        }
    }
}
