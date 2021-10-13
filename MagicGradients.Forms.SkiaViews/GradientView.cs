using MagicGradients.Forms.SkiaViews.Drawing;
using MagicGradients.Masks;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace MagicGradients.Forms.SkiaViews
{
    [ContentProperty(nameof(GradientSource))]
    public class GradientView : SKCanvasView, IGradientControl, IGradientVisualElement
    {
        static GradientView()
        {
            StyleSheetsConfig.RegisterStyles<GradientView>();
        }

        public GradientDrawable<GradientView> Drawable { get; protected set; }

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

        public GradientView()
        {
            Drawable = new GradientDrawable<GradientView>(this);
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

            var context = new DrawContext(e);
            context.Measure(GradientSize, Width);

            Drawable.Draw(context);
        }

        public void InvalidateCanvas()
        {
            InvalidateSurface();
        }
    }
}
