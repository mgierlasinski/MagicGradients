using MagicGradients.Masks;
using MagicGradients.Renderers;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;
using SKCanvasView = SkiaSharp.Views.Forms.SKCanvasView;
//using SKCanvasView = SkiaSharp.Views.Forms.SKGLView;

namespace MagicGradients
{
    [ContentProperty(nameof(GradientSource))]
    public class GradientView : SKCanvasView, IGradientVisualElement
    {
        static GradientView()
        {
            StyleSheets.RegisterStyle("background", typeof(GradientView), nameof(GradientSourceProperty));
            StyleSheets.RegisterStyle("background-size", typeof(GradientView), nameof(GradientSizeProperty));
            StyleSheets.RegisterStyle("background-repeat", typeof(GradientView), nameof(GradientRepeatProperty));
        }

        public GradientRenderer Renderer { get; protected set; }

        public static readonly BindableProperty GradientSourceProperty = BindableProperty.Create(nameof(GradientSource), 
            typeof(IGradientSource), typeof(GradientView), propertyChanged: OnGradientElementChanged);

        public static readonly BindableProperty GradientSizeProperty = BindableProperty.Create(nameof(GradientSize),
            typeof(Dimensions), typeof(GradientView), propertyChanged: UpdateCanvas);

        public static readonly BindableProperty GradientRepeatProperty = BindableProperty.Create(nameof(GradientRepeat),
            typeof(BackgroundRepeat), typeof(GradientView), propertyChanged: UpdateCanvas);

        public static readonly BindableProperty MaskProperty = BindableProperty.Create(nameof(Mask),
            typeof(GradientMask), typeof(GradientView), propertyChanged: OnGradientElementChanged);

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
            Renderer = new GradientRenderer(this);
        }

        private static void OnGradientElementChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var gradientView = (GradientView)bindable;

            if (oldValue != null && oldValue is GradientElement oldElem)
                oldElem.Parent = null;

            if (newValue != null && newValue is GradientElement newElem)
                newElem.Parent = gradientView;

            gradientView.InvalidateCanvas();
        }

        static void UpdateCanvas(BindableObject bindable, object oldValue, object newValue)
        {
            var gradientView = (GradientView)bindable;
            gradientView.InvalidateSurface();
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

            var canvas = e.Surface.Canvas;
            canvas.Clear();

            if (GradientSource == null)
                return;

            var context = Renderer.CreateContext(e);
            Renderer.RenderGradients(context);
        }

        //protected override void OnPaintSurface(SKPaintGLSurfaceEventArgs e)
        //{
        //    base.OnPaintSurface(e);

        //    var canvas = e.Surface.Canvas;
        //    canvas.Clear();

        //    if (GradientSource == null)
        //        return;

        //    var context = Renderer.CreateContext(e);
        //    Renderer.RenderGradients(context);
        //}

        public void InvalidateCanvas()
        {
            InvalidateSurface();
        }
    }
}
