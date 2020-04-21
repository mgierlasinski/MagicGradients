using System;
using MagicGradients.Renderers;
using Xamarin.Forms;

namespace MagicGradients
{
    public class LinearGradient : Gradient
    {
        private readonly LinearGradientRenderer _renderer;

        public static readonly BindableProperty AngleProperty = BindableProperty.Create(
            nameof(Angle), typeof(double), typeof(LinearGradient), 0d);

        public double Angle
        {
            get => (double)GetValue(AngleProperty);
            set => SetValue(AngleProperty, value);
        }

        public LinearGradient()
        {
            _renderer = new LinearGradientRenderer(this);
        }

        public override void Measure(int width, int height)
        {
            base.Measure(width, height);

            foreach (var stop in Stops)
            {
                if (stop.RenderOffset > 1)
                {
                    // Convert pixels to proportion
                    stop.RenderOffset = GetOffsetFromPixels(stop.RenderOffset, width, height);
                }
            }
        }

        private float GetOffsetFromPixels(float offset, int width, int height)
        {
            // Here the Pythagorean Theorem + Trigonometry is applied
            // to figure out the length of the gradient, which is needed to accurately calculate offset.
            // https://en.wikibooks.org/wiki/Trigonometry/The_Pythagorean_Theorem
            var angleRad = GradientMath.ToRadians(Angle);
            var computedLength = Math.Sqrt(Math.Pow(width * Math.Cos(angleRad), 2) + Math.Pow(height * Math.Sin(angleRad), 2));

            return (float)(offset / computedLength);
        }

        public override void Render(RenderContext context)
        {
            _renderer.Render(context);
        }
    }
}
