using Android.Content;
using Android.Content.Res;
using Android.Util;
using MagicGradients.Converters;
using MagicGradients.Masks;

namespace MagicGradients.Platform
{
    public class AttributeReader
    {
        enum MaskShape { Rectangle = 0, Ellipse = 1, Path = 2 }

        public void ReadAttributes(IGradientControl view, Context context, IAttributeSet attrs)
        {
            var typedArray = context.ObtainStyledAttributes(attrs, Resource.Styleable.GradientView);

            try
            {
                ReadGradient(view, typedArray);

                if (typedArray.HasValue(Resource.Styleable.GradientView_maskShape))
                    ReadMask(view, typedArray);
            }
            finally
            {
                typedArray.Recycle();
            }
        }

        private void ReadGradient(IGradientControl view, TypedArray typedArray)
        {
            var source = typedArray.GetString(Resource.Styleable.GradientView_gradientSource);
            if (source != null)
            {
                var converter = new CssGradientSourceTypeConverter();
                view.GradientSource = (IGradientSource)converter.ConvertFromInvariantString(source);
            }

            var size = typedArray.GetString(Resource.Styleable.GradientView_gradientSize);
            if (size != null)
            {
                var converter = new DimensionsTypeConverter();
                view.GradientSize = (Dimensions)converter.ConvertFromInvariantString(size);
            }

            var repeat = typedArray.GetInt(Resource.Styleable.GradientView_gradientRepeat, 0);
            view.GradientRepeat = (BackgroundRepeat)repeat;
        }

        private void ReadMask(IGradientControl view, TypedArray typedArray)
        {
            var maskShape = (MaskShape)typedArray.GetInt(Resource.Styleable.GradientView_maskShape, 0);
            GradientMask mask = maskShape switch
            {
                MaskShape.Rectangle => new RectangleMask
                {
                    Size = GetSize(typedArray),
                    Corners = GetCorners(typedArray)
                },
                MaskShape.Ellipse => new EllipseMask { Size = GetSize(typedArray) },
                MaskShape.Path => new PathMask(typedArray.GetString(Resource.Styleable.GradientView_maskData)),
                _ => null
            };

            if (mask != null)
            {
                mask.Stretch = (Stretch)typedArray.GetInt(Resource.Styleable.GradientView_maskStretch, 0);
                view.Mask = mask;
            }
        }

        private Dimensions GetSize(TypedArray typedArray)
        {
            var size = typedArray.GetString(Resource.Styleable.GradientView_maskSize);
            if (size == null)
                return Dimensions.Prop(1, 1);

            return (Dimensions)new DimensionsTypeConverter().ConvertFrom(size);
        }

        private Corners GetCorners(TypedArray typedArray)
        {
            var corners = typedArray.GetString(Resource.Styleable.GradientView_maskCorners);
            if (corners == null)
                return Corners.Zero;

            return (Corners)new CornersTypeConverter().ConvertFrom(corners);
        }
    }
}
