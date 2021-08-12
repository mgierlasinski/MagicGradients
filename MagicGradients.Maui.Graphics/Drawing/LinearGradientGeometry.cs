using System;
using Microsoft.Maui.Graphics;

namespace MagicGradients.Maui.Graphics.Drawing
{
    public class LinearGradientGeometry
    {
        public PointF Start { get; }
        public PointF End { get; }
        public double Length { get; }
        public double Angle { get; }

        public LinearGradientGeometry(RectangleF boxBounds, double angleDegrees)
        {
            // Calculation
            // https://medium.com/@patrickbrosset/do-you-really-understand-css-linear-gradients-631d9a895caf

            var angleRadians = GradientMath.ToRadians(GradientMath.FromDegrees(angleDegrees));

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
