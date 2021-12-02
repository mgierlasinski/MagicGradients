//using Microsoft.Maui.Graphics;
//using System.Collections.Generic;

//namespace MagicGradients
//{
//    public class Gradient : IGradient
//    {
//        public bool IsRepeating { get; set; }
//        public List<IGradientStop> Stops { get; set; }
//        public IReadOnlyList<IGradientStop> GetStops() => Stops;
//    }

//    public class GradientStop : IGradientStop
//    {
//        public Color Color { get; set; }
//        public Offset Offset { get; set; }
//        public float RenderOffset { get; set; }
//    }

//    public class LinearGradient : Gradient, ILinearGradient
//    {
//        public double Angle { get; set; }
//    }

//    public class RadialGradient : Gradient, IRadialGradient
//    {
//        public Point Center { get; set; }
//        public double RadiusX { get; set; }
//        public double RadiusY { get; set; }
//        public RadialGradientFlags Flags { get; set; }
//        public RadialGradientShape Shape { get; set; }
//        public RadialGradientSize Size { get; set; }
//    }
//}
