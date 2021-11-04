using MagicGradients.Drawing;
using MagicGradients.Forms.Skia.Drawing;
using MagicGradients.Masks;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace MagicGradients.Forms.Skia
{
    [ContentProperty(nameof(GradientSource))]
    public class SkiaGradientGLView : SKGLView, IGradientControl, IGradientVisualElement
    {
        static SkiaGradientGLView()
        {
            GlobalSetup.Current
                .UseXamlGradients()
                .UseCssStyles<SkiaGradientGLView>();
        }

        public GradientDrawable Drawable { get; protected set; }

        public static readonly BindableProperty GradientSourceProperty = GradientControl.GradientSourceProperty;
        public static readonly BindableProperty GradientSizeProperty = GradientControl.GradientSizeProperty;
        public static readonly BindableProperty GradientRepeatProperty = GradientControl.GradientRepeatProperty;
        public static readonly BindableProperty MaskProperty = GradientControl.MaskProperty;

        public double ViewWidth => Width;

        public IGradientSource GradientSource
        {
            get => (IGradientSource) GetValue(GradientSourceProperty);
            set => SetValue(GradientSourceProperty, value);
        }

        public Dimensions GradientSize
        {
            get => (Dimensions) GetValue(GradientSizeProperty);
            set => SetValue(GradientSizeProperty, value);
        }

        public BackgroundRepeat GradientRepeat
        {
            get => (BackgroundRepeat) GetValue(GradientRepeatProperty);
            set => SetValue(GradientRepeatProperty, value);
        }

        public IGradientMask Mask
        {
            get => (IGradientMask) GetValue(MaskProperty);
            set => SetValue(MaskProperty, value);
        }

        public SkiaGradientGLView()
        {
            Drawable = new GradientDrawable(this);
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            this.SetBindingContext(BindingContext);
        }

        protected override void OnPaintSurface(SKPaintGLSurfaceEventArgs e)
        {
            base.OnPaintSurface(e);

            var canvas = new SkiaCanvasEx { Canvas = e.Surface.Canvas };
            var rect = e.BackendRenderTarget.Rect.ToRectF();

            Drawable.Draw(canvas, rect);
        }

        public void InvalidateCanvas()
        {
            InvalidateSurface();
        }
    }
}
