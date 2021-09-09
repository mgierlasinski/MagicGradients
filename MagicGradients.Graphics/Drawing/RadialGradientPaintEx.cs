using System;
using Microsoft.Maui.Graphics;

namespace MagicGradients.Graphics.Drawing
{
    public class RadialGradientPaintEx : RadialGradientPaint
    {
        public Size Size { get; set; }
        public bool IsRepeating { get; set; }

        public RadialGradientPaintEx(
            Microsoft.Maui.Graphics.GradientStop[] gradientStops, 
            Point center, 
            Size size, 
            bool isRepeating) : base(gradientStops)
        {
            Center = center;
            Size = size;
            Radius = Math.Max(Size.Width, Size.Height);
            IsRepeating = isRepeating;
        }
    }
}
