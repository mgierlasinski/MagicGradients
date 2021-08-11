//using System;
//using SkiaSharp;

//namespace MagicGradients.Skia.Forms.Drawing
//{
//    public class GradientLine
//    {
//        public SKPoint Start { get; set; }
//        public SKPoint End { get; set; }
//        public double Length { get; set; }
//        public double Angle { get; set; }

//        public GradientLine(SKRectI boxBounds, double angleDegrees)
//        {
//            // Calculation
//            // https://medium.com/@patrickbrosset/do-you-really-understand-css-linear-gradients-631d9a895caf

//            var angleRadians = GradientMath.ToRadians(GradientMath.FromDegrees(angleDegrees));

//            var lineLength =
//                Math.Abs(boxBounds.Width * Math.Sin(angleRadians)) +
//                Math.Abs(boxBounds.Height * Math.Cos(angleRadians));

//            var center = new SKPoint(boxBounds.MidX, boxBounds.MidY);

//            var yDiff = (float)(Math.Sin(angleRadians - Math.PI / 2) * lineLength / 2);
//            var xDiff = (float)(Math.Cos(angleRadians - Math.PI / 2) * lineLength / 2);

//            Start = new SKPoint(center.X - xDiff, center.Y - yDiff);
//            End = new SKPoint(center.X + xDiff, center.Y + yDiff);
//            Length = lineLength;
//            Angle = angleRadians;
//        }

//        public SKPoint GetColorPointAt(float position)
//        {
//            var yDiff = Math.Sin(Angle - Math.PI / 2) * (Length * position);
//            var xDiff = Math.Cos(Angle - Math.PI / 2) * (Length * position);

//            return new SKPoint(Start.X + (float)xDiff, Start.Y + (float)yDiff);
//        }
//    }
//}
