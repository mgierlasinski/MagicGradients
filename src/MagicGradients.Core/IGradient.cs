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
        Position Center { get; }
        Dimensions Radius { get; }
        RadialGradientShape Shape { get; }
        RadialGradientStretch Stretch { get; }
    }
}
