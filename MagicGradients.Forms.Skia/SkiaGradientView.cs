using MagicGradients.Drawing;
using MagicGradients.Forms.Skia.Drawing;
using MagicGradients.Forms.Skia.Masks;
using MagicGradients.Masks;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace MagicGradients.Forms.Skia
{
    [ContentProperty(nameof(GradientSource))]
    public class SkiaGradientView : SKCanvasView, IGradientControl, IGradientVisualElement
    {
        static SkiaGradientView()
        {
            GlobalSetup.Current
                .UseXamlGradients()
                .UseCssStyles<SkiaGradientView>();
        }

        public GradientDrawable Drawable { get; }

        public static readonly BindableProperty GradientSourceProperty = GradientControl.GradientSourceProperty;
        public static readonly BindableProperty GradientSizeProperty = GradientControl.GradientSizeProperty;
        public static readonly BindableProperty GradientRepeatProperty = GradientControl.GradientRepeatProperty;
        public static readonly BindableProperty MaskProperty = GradientControl.MaskProperty;

        public double ViewWidth => Width;

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

        public IGradientMask Mask
        {
            get => (IGradientMask)GetValue(MaskProperty);
            set => SetValue(MaskProperty, value);
        }

        public SkiaGradientView()
        {
            Drawable = new GradientDrawable(this);
            Drawable.MaskDrawable.RectanglePainter = new SkiaRectangleMaskPainter();
            Drawable.MaskDrawable.EllipsePainter = new SkiaEllipseMaskPainter();
            Drawable.MaskDrawable.PathPainter = new SkiaPathMaskPainter();
            Drawable.MaskDrawable.TextPainter = new SkiaTextMaskPainter();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            this.SetBindingContext(BindingContext);
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
