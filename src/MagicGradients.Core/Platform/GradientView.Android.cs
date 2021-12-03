﻿using Android.Content;
using Android.Util;
using MagicGradients.Drawing;
using MagicGradients.Platform;
using Microsoft.Maui.Graphics.Native;

namespace MagicGradients
{
    public partial class GradientView : NativeGraphicsView, IGradientControl
    {
        private readonly AttributeReader _attributeReader = new AttributeReader();
        public double ViewWidth => Width;

        public GradientView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Drawable = new GradientDrawable(this);
            _attributeReader.ReadAttributes(this, context, attrs);
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
