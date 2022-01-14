using FluentAssertions;
using FluentAssertions.Execution;
using MagicGradients.Builder;
using Microsoft.Maui.Graphics;
using Xunit;

namespace MagicGradients.Core.Tests.Builder
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
            builder.AddStop(Colors.White);
            builder.AddStop(Colors.Black);

            var gradients = builder.Build();

            // Assert
            using (new AssertionScope())
            {
                gradients.Should().HaveCount(1);
                gradients[0].GetStops().Should().HaveCount(2);
            }
        }

        [Theory]
        [ClassData(typeof(GradientBuilderTestCases))]
        public void AddStops_AndGradient_AndStops_TwoGradientsWithStops(GradientBuilderTestCase testCase)
        {
            // Arrange
            var builder = new GradientBuilder();

            // Act
            builder.AddStop(Colors.Red);
            testCase.AddGradientTo(builder);
            builder.AddStop(Colors.White);
            builder.AddStop(Colors.Black);

            var gradients = builder.Build();

            // Assert
            using (new AssertionScope())
            {
                gradients.Should().HaveCount(2);
                gradients[0].GetStops().Should().HaveCount(1);
                gradients[1].GetStops().Should().HaveCount(2);
            }
        }

        [Fact]
        public void AddOnlyStops_DefaultGradientWithStops()
        {
            // Arrange
            var builder = new GradientBuilder();

            // Act
            builder.AddStop(Colors.White);
            builder.AddStop(Colors.Black);

            var gradients = builder.Build();

            // Assert
            using (new AssertionScope())
            {
                gradients.Should().HaveCount(1);
                gradients[0].Should().BeOfType<LinearGradient>();
                gradients[0].GetStops().Should().HaveCount(2);
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
                    .AddStop(Colors.Red, Offset.Prop(0.2))
                    .AddStop(Colors.Blue, Offset.Prop(0.4)))
                .AddRadialGradient(g => g
                    .Circle().At(0.5, 0.5, o => o.Proportional())
                    .Resize(200, 200, o => o.Absolute())
                    .StretchTo(RadialGradientSize.FarthestSide)
                    .Repeat()
                    .AddStops(Colors.Red, Colors.Green, Colors.Blue))
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
                radial.Center.Should().BeEquivalentTo(Position.Prop(0.5, 0.5));
                radial.Radius.Should().BeEquivalentTo(Dimensions.Zero);
                radial.Shape.Should().Be(RadialGradientShape.Ellipse);
                radial.Size.Should().Be(RadialGradientSize.FarthestCorner);
                radial.IsRepeating.Should().BeFalse();
            }
        }

        [Fact]
        public void FluentApi_BuildSource_GenericGradientWithGradients()
        {
            // Act
            var source = new GradientBuilder()
                .AddLinearGradient(g => g
                    .Rotate(20)
                    .AddStops(Colors.Red, Colors.Green, Colors.Blue))
                .BuildSource();

            // Assert
            using (new AssertionScope())
            {
                source.Should().BeOfType<GenericGradientSource>();
                source.GetGradients().Should().HaveCount(1);
            }
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void AddCssGradient_EmptyCss_NothingBuilt(string css)
        {
            // Arrange & Act
            var gradients = new GradientBuilder()
                .AddCssGradient(css)
                .Build();
            
            // Assert
            gradients.Should().BeEmpty();
        }

        [Fact]
        public void AddCssGradient_ValidCss_GradientParsed()
        {
            // Arrange & Act
            var gradients = new GradientBuilder()
                .AddCssGradient("linear-gradient(red, orange)")
                .Build();

            // Assert
            using (new AssertionScope())
            {
                gradients.Should().HaveCount(1);
                gradients[0].Should().BeOfType<LinearGradient>();
                gradients[0].GetStops().Should().HaveCount(2);
                gradients[0].GetStops()[0].Color.Should().Be(Colors.Red);
                gradients[0].GetStops()[1].Color.Should().Be(Colors.Orange);
            }
        }
        
        [Fact]
        public void AddCssGradient_MultipleCssGradients_MultipleGradientsBuilt()
        {
            // Arrange & Act
            var gradients = new GradientBuilder()
                .AddCssGradient("linear-gradient(black,white), radial-gradient(pink,brown)")
                .Build();
            
            // Assert
            using (new AssertionScope())
            {
                gradients.Should().HaveCount(2);
                gradients[0].Should().BeOfType<RadialGradient>();
                gradients[1].Should().BeOfType<LinearGradient>();
            }
        }
    }
}
