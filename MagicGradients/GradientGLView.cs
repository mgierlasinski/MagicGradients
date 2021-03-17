using MagicGradients.Masks;
using MagicGradients.Renderers;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace MagicGradients
{
    [ContentProperty(nameof(GradientSource))]
    public class GradientGLView : SKGLView, IGradientControl, IGradientVisualElement
    {
        public GradientRenderer<GradientGLView> Renderer { get; protected set; }

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

        public GradientGLView()
        {
            Renderer = new GradientRenderer<GradientGLView>(this);
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

        protected override void OnPaintSurface(SKPaintGLSurfaceEventArgs e)
        {
            base.OnPaintSurface(e);

            var canvas = e.Surface.Canvas;
            canvas.Clear();

            if (GradientSource != null)
            {
                var context = Renderer.CreateContext(e);
                Renderer.PaintSurface(context);
            }
        }

        public void InvalidateCanvas()
        {
            InvalidateSurface();
        }
    }
}
