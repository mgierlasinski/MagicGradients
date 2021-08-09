using MagicGradients.Renderers;
using SkiaSharp;
using Xamarin.Forms;

namespace MagicGradients.Masks
{
    public class TextMask : PathMask
    {
        public const double DefaultFontSize = 18d;

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text),
            typeof(string), typeof(TextMask));

        public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(nameof(FontFamily), 
            typeof(string), typeof(TextMask), default(string));

        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize),
            typeof(double), typeof(TextMask), DefaultFontSize);

        public static readonly BindableProperty FontAttributesProperty = BindableProperty.Create(nameof(FontAttributes), 
            typeof(FontAttributes), typeof(TextMask), FontAttributes.None);
        
        public static readonly BindableProperty HorizontalTextAlignmentProperty = BindableProperty.Create(nameof(HorizontalTextAlignment), 
            typeof(TextAlignment), typeof(TextMask), TextAlignment.Center);

        public static readonly BindableProperty VerticalTextAlignmentProperty = BindableProperty.Create(nameof(VerticalTextAlignment), 
            typeof(TextAlignment), typeof(TextMask), TextAlignment.Center);

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public string FontFamily
        {
            get => (string)GetValue(FontFamilyProperty);
            set => SetValue(FontFamilyProperty, value);
        }

        [TypeConverter(typeof(FontSizeConverter))]
        public double FontSize
        {
            get => (double)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        public FontAttributes FontAttributes
        {
            get => (FontAttributes)GetValue(FontAttributesProperty);
            set => SetValue(FontAttributesProperty, value);
        }

        public TextAlignment HorizontalTextAlignment
        {
            get => (TextAlignment)GetValue(HorizontalTextAlignmentProperty);
            set => SetValue(HorizontalTextAlignmentProperty, value);
        }

        public TextAlignment VerticalTextAlignment
        {
            get => (TextAlignment)GetValue(VerticalTextAlignmentProperty);
            set => SetValue(VerticalTextAlignmentProperty, value);
        }

        public override void Clip(RenderContext context)
        {
            if (!IsActive)
                return;

            using var textPaint = GetTextPaint(context);
            using var textPath = textPaint.GetTextPath(Text, 0, 0);

            // Make sure it starts from (0, 0)
            //if (textPath.Bounds.Top != 0 || textPath.Bounds.Left != 0)
            //{
            //    textPath.Transform(SKMatrix.MakeTranslation(-textPath.Bounds.Left, -textPath.Bounds.Top));
            //}
            
            ClipPath(context, textPath);
        }

        private SKPaint GetTextPaint(RenderContext context)
        {
            var isBold = (FontAttributes & FontAttributes.Bold) == FontAttributes.Bold;
            var isItalic = (FontAttributes & FontAttributes.Italic) == FontAttributes.Italic;

            var fontStyle = isBold && isItalic ? SKFontStyle.BoldItalic 
                : isBold ? SKFontStyle.Bold 
                : isItalic ? SKFontStyle.Italic : 
                SKFontStyle.Normal;

            return new SKPaint
            {
                TextSize = (float)(FontSize * context.PixelScaling),
                Typeface = SKTypeface.FromFamilyName(FontFamily, fontStyle),
                IsAntialias = true
            };
        }

        protected override void BeginLayout(RenderContext context, SKRect bounds)
        {
            var posX = HorizontalTextAlignment switch
            {
                TextAlignment.Center => (float)context.RenderRect.Width / 2,
                TextAlignment.End => context.RenderRect.Width,
                _ => 0
            };

            var posY = VerticalTextAlignment switch
            {
                TextAlignment.Center => (float)context.RenderRect.Height / 2,
                TextAlignment.End => context.RenderRect.Height,
                _ => 0
            };

            context.Canvas.Translate(posX, posY);
        }

        protected override void EndLayout(RenderContext context, SKRect bounds)
        {
            var movX = HorizontalTextAlignment switch
            {
                TextAlignment.Center => -bounds.MidX,
                TextAlignment.End => -bounds.Right,
                _ => -bounds.Left
            };

            var movY = VerticalTextAlignment switch
            {
                TextAlignment.Center => -bounds.MidY,
                TextAlignment.End => -bounds.Bottom,
                _ => -bounds.Top
            };

            context.Canvas.Translate(movX, movY);
        }

        //protected override void LayoutBounds(RenderContext context, SKRect bounds, bool keepAspectRatio)
        //{
        //    if (Stretch != Stretch.None)
        //    {
        //        base.LayoutBounds(context, bounds, keepAspectRatio);
        //        return;
        //    }

        //    var scaleX = (float)context.RenderRect.Width / context.CanvasRect.Width;
        //    var scaleY = (float)context.RenderRect.Height / context.CanvasRect.Height;
        //    var scale = Math.Max(scaleX, scaleY);

        //    var posX = HorizontalTextAlignment switch
        //    {
        //        TextAlignment.Center => (float)context.RenderRect.Width / 2 - bounds.MidX * scale,
        //        TextAlignment.End => context.RenderRect.Width - bounds.Right * scale,
        //        _ => -bounds.Left * scale
        //    };

        //    var posY = VerticalTextAlignment switch
        //    {
        //        TextAlignment.Center => (float)context.RenderRect.Height / 2 - bounds.MidY * scale,
        //        TextAlignment.End => context.RenderRect.Height - bounds.Bottom * scale,
        //        _ => -bounds.Top * scale
        //    };

        //    context.Canvas.Translate(posX, posY);
        //    context.Canvas.Scale(scale);
        //}

        //protected override void LayoutBounds(RenderContext context, SKRect bounds, bool keepAspectRatio)
        //{
        //    if (Stretch != Stretch.None)
        //    {
        //        base.LayoutBounds(context, bounds, keepAspectRatio);
        //        return;
        //    }

        //    var scaleX = (float)context.RenderRect.Width / context.CanvasRect.Width;
        //    var scaleY = (float)context.RenderRect.Height / context.CanvasRect.Height;
        //    var scale = Math.Max(scaleX, scaleY);

        //    var posX = HorizontalTextAlignment switch
        //    {
        //        TextAlignment.Center => (float)context.RenderRect.Width / 2,
        //        TextAlignment.End => context.RenderRect.Width,
        //        _ => 0
        //    };

        //    var movX = HorizontalTextAlignment switch
        //    {
        //        TextAlignment.Center => -bounds.MidX,
        //        TextAlignment.End => -bounds.Width,
        //        _ => 0
        //    };

        //    var posY = VerticalTextAlignment switch
        //    {
        //        TextAlignment.Center => (float)context.RenderRect.Height / 2,
        //        TextAlignment.End => context.RenderRect.Height,
        //        _ => 0
        //    };

        //    var movY = VerticalTextAlignment switch
        //    {
        //        TextAlignment.Center => -bounds.MidY,
        //        TextAlignment.End => -bounds.Height,
        //        _ => 0
        //    };

        //    context.Canvas.Translate(posX, posY);
        //    context.Canvas.Scale(scale);
        //    context.Canvas.Translate(movX, movY);
        //}
    }
}