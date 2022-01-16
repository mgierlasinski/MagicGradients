using MagicGradients.Drawing;
using Microsoft.Maui.Graphics.Native;

namespace MagicGradients
{
    public partial class GradientView : NativeGraphicsView
    {
        public GradientView()
        {
            Drawable = new GradientDrawable(this);
            _getWidth = () => Bounds.Width;
        }

        partial void InvalidateNative()
        {
            InvalidateDrawable();
        }
    }
}
