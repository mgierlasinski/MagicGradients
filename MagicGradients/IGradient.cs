using System.Collections.Generic;
using SkiaSharp;

namespace MagicGradients
{
    public interface IGradient
    {
        IList<ColorStop> Stops { get; }

        SKShader CreateShader(SKPaint paint, SKImageInfo info);
    }
}
