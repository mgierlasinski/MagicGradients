using Microsoft.Maui.Graphics;
using System;

namespace MagicGradients.Drawing
{
    public class LinearGradientGeometry : GradientGeometry<ILinearGradient>
    {
        public PointF Start { get; private set; }
        public PointF End { get; private set; }
        public double Length { get; private set; }
        public double Angle { get; private set; }
        
        protected override double CalculateRenderOffset(ILinearGradient gradient, double offset, int width, int height)
        {
            // Here the Pythagorean Theorem + Trigonometry is applied
            // to figure out the length of the gradient, which is needed to accurately calculate offset.
            // https://en.wikibooks.org/wiki/Trigonometry/The_Pythagorean_Theorem

            var angleDeg = gradient.Angle;
            var angleRad = GradientMath.ToRadians(angleDeg);
            var computedLength = Math.Sqrt(Math.Pow(width * Math.Cos(angleRad), 2) + Math.Pow(height * Math.Sin(angleRad), 2));

            return computedLength != 0 ? offset / computedLength : 1;
        }

        public void CalculateGeometry(ILinearGradient gradient, RectangleF boxBounds)
        {
            // Calculation
            // https://medium.com/@patrickbrosset/do-you-really-understand-css-linear-gradients-631d9a895caf

            var angleDegrees = gradient.Angle;
            var angleRadians = GradientMath.ToRadians(GradientMath.RotateBy180(angleDegrees));

            var lineLength =
                Math.Abs(boxBounds.Width * Math.Sin(angleRadians)) +
                Math.Abs(boxBounds.Height * Math.Cos(angleRadians));

            var center = boxBounds.Center;

            var yDiff = (float)(Math.Sin(angleRadians - Math.PI / 2) * lineLength / 2);
            var xDiff = (float)(Math.Cos(angleRadians - Math.PI / 2) * lineLength / 2);

            Start = new PointF(center.X - xDiff, center.Y - yDiff);
            End = new PointF(center.X + xDiff, center.Y + yDiff);
            Length = lineLength;
            Angle = angleRadians;
        }

        public PointF GetColorPointAt(float position)
        {
            var yDiff = Math.Sin(Angle - Math.PI / 2) * (Length * position);
            var xDiff = Math.Cos(Angle - Math.PI / 2) * (Length * position);

            return new PointF(Start.X + (float)xDiff, Start.Y + (float)yDiff);
        }

        public float ScaleWithBias(float input, float inLow, float inHigh, float outLow, float outHigh)
        {
            // Calculation
            // https://gamedev.stackexchange.com/questions/33441/how-to-convert-a-number-from-one-min-max-set-to-another-min-max-set
            return (input - inLow) / (inHigh - inLow) * (outHigh - outLow) + outLow;
        }
    }
}
