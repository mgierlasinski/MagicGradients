using MagicGradients.Renderers;
using System;
using Xamarin.Forms;

namespace MagicGradients
{
    public class LinearGradient : Gradient
    {
        public static readonly BindableProperty AngleProperty = BindableProperty.Create(
            nameof(Angle), typeof(double), typeof(LinearGradient), 0d);

        public double Angle
        {
            get => (double)GetValue(AngleProperty);
            set => SetValue(AngleProperty, value);
        }

        public LinearGradient()
        {
            Shader = new LinearGradientShader(this);
        }

//        public override void Render(RenderContext context)
//        {
//#if DEBUG_RENDER
//            System.Diagnostics.Debug.WriteLine($"Rendering Linear Gradient with {Stops.Count} stops");
//#endif
//            _renderer.Render(context, _shader);
//        }

        protected override double CalculateRenderOffset(double offset, int width, int height)
        {
            // Here the Pythagorean Theorem + Trigonometry is applied
            // to figure out the length of the gradient, which is needed to accurately calculate offset.
            // https://en.wikibooks.org/wiki/Trigonometry/The_Pythagorean_Theorem
            var angleRad = GradientMath.ToRadians(Angle);
            var computedLength = Math.Sqrt(Math.Pow(width * Math.Cos(angleRad), 2) + Math.Pow(height * Math.Sin(angleRad), 2));

            return computedLength != 0 ? offset / computedLength : 1;
        }
    }
}
