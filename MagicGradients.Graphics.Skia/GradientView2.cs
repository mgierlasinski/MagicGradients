using MagicGradients.Graphics.Skia.Drawing;
using MagicGradients.Graphics.Skia.Masks;
using MagicGradients.Masks;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace MagicGradients.Graphics.Skia
{
    [ContentProperty(nameof(GradientSource))]
    public class GradientView2 : SKCanvasView, IGradientControl, IGradientVisualElement
    {
        static GradientView2()
        {
            StyleSheets.RegisterStyle("background", typeof(GradientView2), nameof(GradientControl.GradientSourceProperty));
            StyleSheets.RegisterStyle("background-size", typeof(GradientView2), nameof(GradientControl.GradientSourceProperty));
            StyleSheets.RegisterStyle("background-repeat", typeof(GradientView2), nameof(GradientControl.GradientSourceProperty));
        }

        public Maui.Graphics.Drawing.GradientDrawable<GradientView2> Drawable { get; }

        public static readonly BindableProperty GradientSourceProperty = GradientControl.GradientSourceProperty;
        public static readonly BindableProperty GradientSizeProperty = GradientControl.GradientSizeProperty;
        public static readonly BindableProperty GradientRepeatProperty = GradientControl.GradientRepeatProperty;
        public static readonly BindableProperty MaskProperty = GradientControl.MaskProperty;

        public IGradientSource GradientSource
        {
            get => (IGradientSource)GetValue(GradientSourceProperty);
            set => SetValue(GradientSourceProperty, value);
        }

        public Dimensions GradientSize
        {
            get => (Dimensions)GetValue(GradientSizeProperty);
            set => SetValue(GradientSizeProperty, value);
        }

        public BackgroundRepeat GradientRepeat
        {
            get => (BackgroundRepeat)GetValue(GradientRepeatProperty);
            set => SetValue(GradientRepeatProperty, value);
        }

        public GradientMask Mask
        {
            get => (GradientMask)GetValue(MaskProperty);
            set => SetValue(MaskProperty, value);
        }

        public GradientView2()
        {
            Drawable = new Maui.Graphics.Drawing.GradientDrawable<GradientView2>(this);
            Drawable.MaskDrawable.PathPainter = new SkiaPathMaskPainter();
            Drawable.MaskDrawable.TextPainter = new SkiaTextMaskPainter();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (GradientSource != null && GradientSource is BindableObject bindable)
            {
                SetInheritedBindingContext(bindable, BindingContext);
            }

            if (Mask != null)
            {
                SetInheritedBindingContext(Mask, BindingContext);
            }
        }

        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            base.OnPaintSurface(e);

            var canvas = new SkiaCanvasEx {Canvas = e.Surface.Canvas};
            var rect = e.Info.Rect.ToRectF();
            
            Drawable.Draw(canvas, rect);
        }

        public void InvalidateCanvas()
        {
            InvalidateSurface();
        }
    }
}