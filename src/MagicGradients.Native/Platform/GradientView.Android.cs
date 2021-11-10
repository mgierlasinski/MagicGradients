using Android.Content;
using Android.Util;
using MagicGradients.Converters;
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
            ParseAttributes(context, attrs);
        }

        public GradientView(Context context) : base(context)
        {
            Drawable = new GradientDrawable(this);
        }

        private void ParseAttributes(Context context, IAttributeSet attrs)
        {
            var typedArray = context.ObtainStyledAttributes(attrs, Resource.Styleable.GradientView);

            try
            {
                var source = typedArray.GetString(Resource.Styleable.GradientView_gradientSource);
                if (source != null)
                {
                    var converter = new CssGradientSourceTypeConverter();
                    _gradientSource = (IGradientSource)converter.ConvertFromInvariantString(source);
                }
                var size = typedArray.GetString(Resource.Styleable.GradientView_gradientSize);
                if (size != null)
                {
                    var converter = new DimensionsTypeConverter();
                    _gradientSize = (Dimensions)converter.ConvertFromInvariantString(size);
                }
                var repeat = typedArray.GetInt(Resource.Styleable.GradientView_gradientRepeat, 0);
                _gradientRepeat = (BackgroundRepeat)repeat;
            }
            finally
            {
                typedArray.Recycle();
            }
        }

        partial void InvalidateNative()
        {
            Invalidate();
        }
    }
}
