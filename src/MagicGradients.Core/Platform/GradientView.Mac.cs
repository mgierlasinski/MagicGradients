using MagicGradients.Drawing;
using Microsoft.Maui.Graphics.Platform;

namespace MagicGradients
{
    public partial class GradientView : PlatformGraphicsView
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
