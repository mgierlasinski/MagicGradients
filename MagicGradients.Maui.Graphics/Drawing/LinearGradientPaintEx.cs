using Microsoft.Maui.Graphics;

namespace MagicGradients.Maui.Graphics.Drawing
{
    public class LinearGradientPaintEx : LinearGradientPaint
    {
        public bool IsRepeating { get; set; }

        public LinearGradientPaintEx(
            Microsoft.Maui.Graphics.GradientStop[] gradientStops, 
            Point startPoint, 
            Point endPoint, 
            bool isRepeating) : base(gradientStops, startPoint, endPoint)
        {
            IsRepeating = isRepeating;
        }
    }
}
