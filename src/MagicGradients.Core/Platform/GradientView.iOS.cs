using MagicGradients.Drawing;
using Microsoft.Maui.Graphics.Native;

namespace MagicGradients
{
    public partial class GradientView : NativeGraphicsView
    {
        public GradientView()
        {
            // TODO: set ViewWidth to Bounds.Width
            Drawable = new GradientDrawable(this);
        }

        partial void InvalidateNative()
        {
            InvalidateDrawable();
        }
    }
}
