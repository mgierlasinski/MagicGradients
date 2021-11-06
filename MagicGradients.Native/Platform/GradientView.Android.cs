using Android.Content;
using Android.Util;
using MagicGradients.Drawing;
using Microsoft.Maui.Graphics.Native;

namespace MagicGradients
{
    public partial class GradientView : NativeGraphicsView, IGradientControl
    {
        public double ViewWidth => Width;

        public GradientView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Drawable = new GradientDrawable(this);
        }

        public GradientView(Context context) : base(context)
        {
            Drawable = new GradientDrawable(this);
        }
        
        partial void InvalidateNative()
        {
            Invalidate();
        }
    }
}
