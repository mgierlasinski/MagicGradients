using Android.Content;
using Android.Content.Res;
using Android.Util;
using MagicGradients.Converters;
using MagicGradients.Drawing;
using MagicGradients.Masks;
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
                ParseGradient(typedArray);

                if (typedArray.HasValue(Resource.Styleable.GradientView_maskShape))
                    ParseMask(typedArray);
            }
            finally
            {
                typedArray.Recycle();
            }
        }

        private void ParseGradient(TypedArray typedArray)
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

        private void ParseMask(TypedArray typedArray)
        {
            var maskShape = typedArray.GetInt(Resource.Styleable.GradientView_maskShape, 0);
            GradientMask mask = maskShape switch
            {
                0 => new RectangleMask
                {
                    Size = GetSize(),
                    Corners = GetCorners()
                },
                1 => new EllipseMask { Size = GetSize() },
                2 => new PathMask { Data = typedArray.GetString(Resource.Styleable.GradientView_maskData) },
                _ => null
            };
            
            if (mask != null)
            {
                mask.Stretch = (Stretch)typedArray.GetInt(Resource.Styleable.GradientView_maskStretch, 0);
                Mask = mask;
            }

            Dimensions GetSize()
            {
                var size = typedArray.GetString(Resource.Styleable.GradientView_maskSize);
                if(size == null)
                    return Dimensions.Prop(1, 1);

                return (Dimensions)new DimensionsTypeConverter().ConvertFrom(size);
            }

            Corners GetCorners()
            {
                var corners = typedArray.GetString(Resource.Styleable.GradientView_maskCorners);
                if (corners == null)
                    return Corners.Zero;

                return (Corners)new CornersTypeConverter().ConvertFrom(corners);
            }
        }
        
        partial void InvalidateNative()
        {
            Invalidate();
        }
    }
}
