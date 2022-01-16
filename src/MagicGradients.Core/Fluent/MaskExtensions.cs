using MagicGradients.Masks;

namespace MagicGradients.Fluent
{
    public static class MaskExtensions
    {
        public static T ClipMode<T>(this T mask, ClipMode clipMode) where T : GradientMask
        {
            mask.ClipMode = clipMode;
            return mask;
        }

        public static T Stretch<T>(this T mask, Stretch stretch) where T : GradientMask
        {
            mask.Stretch = stretch;
            return mask;
        }

        public static T Size<T>(this T mask, Dimensions size) where T : RectangleMask
        {
            mask.Size = size;
            return mask;
        }

        public static T Size<T>(this T mask, double width, double height, OffsetType type = OffsetType.Absolute) where T : RectangleMask
        {
            mask.Size = new Dimensions(width, height, type);
            return mask;
        }

        public static T Corners<T>(this T mask, Dimensions corners) where T : RectangleMask
        {
            mask.Corners = new Corners(corners);
            return mask;
        }
        
        public static T Corners<T>(this T mask, double corners, OffsetType type = OffsetType.Absolute) where T : RectangleMask
        {
            mask.Corners = new Corners(new Dimensions(corners, corners, type));
            return mask;
        }

        public static T Corners<T>(this T mask, Dimensions? topLeft = null, Dimensions? topRight = null, Dimensions? bottomRight = null, Dimensions? bottomLeft = null) where T : RectangleMask
        {
            mask.Corners = new Corners(
                topLeft ?? Dimensions.Zero,
                topRight ?? Dimensions.Zero,
                bottomRight ?? Dimensions.Zero,
                bottomLeft ?? Dimensions.Zero);
            return mask;
        }

        public static T Corners<T>(this T mask, double topLeft = 0, double topRight = 0, double bottomRight = 0, double bottomLeft = 0, OffsetType type = OffsetType.Absolute) where T : RectangleMask
        {
            mask.Corners = new Corners(
                new Dimensions(topLeft, topLeft, type), 
                new Dimensions(topRight, topRight, type), 
                new Dimensions(bottomRight, bottomRight, type), 
                new Dimensions(bottomLeft, bottomLeft, type));
            return mask;
        }

        //public static TextMask Font(this TextMask mask, string family = null, double size = 0, FontAttributes attributes = MagicGradients.FontAttributes.None)
        //{
        //    if(family != null)
        //        mask.FontFamily = family;

        //    if (size > 0)
        //        mask.FontSize = size;

        //    mask.FontAttributes = attributes;
        //    return mask;
        //}

        public static TextMask FontFamily(this TextMask mask, string fontFamily)
        {
            mask.FontFamily = fontFamily;
            return mask;
        }

        public static TextMask FontSize(this TextMask mask, double fontSize)
        {
            mask.FontSize = fontSize;
            return mask;
        }

        public static TextMask FontAttributes(this TextMask mask, FontAttributes fontAttributes)
        {
            mask.FontAttributes = fontAttributes;
            return mask;
        }

        //public static TextMask TextAlignment(this TextMask mask, TextAlignment horizontal = MagicGradients.TextAlignment.Center, TextAlignment vertical = MagicGradients.TextAlignment.Center)
        //{
        //    mask.HorizontalTextAlignment = horizontal;
        //    mask.VerticalTextAlignment = vertical;
        //    return mask;
        //}

        public static TextMask HorizontalTextAlignment(this TextMask mask, TextAlignment alignment)
        {
            mask.HorizontalTextAlignment = alignment;
            return mask;
        }

        public static TextMask VerticalTextAlignment(this TextMask mask, TextAlignment alignment)
        {
            mask.VerticalTextAlignment = alignment;
            return mask;
        }
    }
}
