using Microsoft.Maui.Graphics;
using System.Collections.Generic;

namespace MagicGradients
{
    public class GradientStop : IGradientStop
    {
        public Color Color { get; }
        public Offset Offset { get; }
        public float RenderOffset { get; set; }

        public GradientStop(Color color, Offset? offset = null)
        {
            Color = color;
            Offset = offset ?? Offset.Empty;
        }
    }

    public class Gradient : IGradient
    {
        public bool IsRepeating { get; set; }
        public List<IGradientStop> Stops { get; set; }
        public IReadOnlyList<IGradientStop> GetStops() => Stops;
    }

    public class LinearGradient : Gradient, ILinearGradient
    {
        public double Angle { get; set; }
    }

    public class RadialGradient : Gradient, IRadialGradient
    {
        public Position Center { get; set; }
        public Dimensions Radius { get; set; }
        public RadialGradientShape Shape { get; set; }
        public RadialGradientSize Size { get; set; }
    }
}
