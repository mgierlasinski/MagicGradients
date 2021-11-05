using MagicGradients.Drawing;
using Microsoft.Maui.Graphics.Native;

namespace MagicGradients
{
    public partial class GradientView : NativeGraphicsView, IGradientControl
    {
        public double ViewWidth => Bounds.Width;

        public GradientView()
        {
            Drawable = new GradientDrawable(this);
        }

        partial void InvalidateNative()
        {
            InvalidateDrawable();
        }
    }
}
