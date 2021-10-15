using Microsoft.Maui.Graphics;
using System.Collections.Generic;

namespace MagicGradients
{
    public interface IGradient
    {
        bool IsRepeating { get; }
        IReadOnlyList<IGradientStop> GetStops();
    }

    public interface IGradientStop
    {
        Color Color { get; }
        Offset Offset { get; }
        float RenderOffset { get; set; }
    }

    public interface ILinearGradient : IGradient
    {
        double Angle { get; }
    }

    public interface IRadialGradient : IGradient
    {
        Point Center { get; }
        double RadiusX { get; }
        double RadiusY { get; }
        RadialGradientFlags Flags { get; }
        RadialGradientShape Shape { get; }
        RadialGradientSize Size { get; }
    }
}
