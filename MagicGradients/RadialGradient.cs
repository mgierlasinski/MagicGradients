using SkiaSharp;
using System;

namespace MagicGradients
{
    public class RadialGradient : Gradient
    {
        public override SKShader CreateShader(SKPaint paint, SKImageInfo info)
        {
            throw new NotImplementedException("Radial gradient is not supported (yet)");
        }
    }
}
