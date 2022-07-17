using Microsoft.Maui.Graphics;

namespace MagicGradients.Drawing
{
    public class LinearGradientPaintEx : LinearGradientPaint
    {
        public bool IsRepeating { get; set; }

        public LinearGradientPaintEx(
            PaintGradientStop[] gradientStops, 
            Point startPoint, 
            Point endPoint, 
            bool isRepeating) : base(gradientStops, startPoint, endPoint)
        {
            IsRepeating = isRepeating;
        }
    }
}
