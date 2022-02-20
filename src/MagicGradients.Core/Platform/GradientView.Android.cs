using Android.Content;
using Android.Util;
using MagicGradients.Drawing;
using MagicGradients.Platform;
using Microsoft.Maui.Graphics.Platform;

namespace MagicGradients
{
    public partial class GradientView : PlatformGraphicsView
    {
        private readonly AttributeReader _attributeReader = new AttributeReader();

        public GradientView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Drawable = new GradientDrawable(this);
            _attributeReader.ReadAttributes(this, context, attrs);
        }

        public GradientView(Context context) : base(context)
        {
            Drawable = new GradientDrawable(this);
        }

        protected override void OnSizeChanged(int width, int height, int oldWidth, int oldHeight)
        {
            ViewWidth = width;
            base.OnSizeChanged(width, height, oldWidth, oldHeight);
        }

        partial void InvalidateNative()
        {
            Invalidate();
        }
    }
}
