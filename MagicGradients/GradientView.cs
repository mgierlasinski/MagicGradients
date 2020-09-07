using MagicGradients.Renderers;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

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
            typeof(IGradientSource), typeof(GradientView), propertyChanged: OnGradientSourceChanged);

        public static readonly BindableProperty GradientSizeProperty = BindableProperty.Create(nameof(GradientSize),
            typeof(Dimensions), typeof(GradientView), propertyChanged: UpdateCanvas);

        public static readonly BindableProperty GradientRepeatProperty = BindableProperty.Create(nameof(GradientRepeat),
            typeof(BackgroundRepeat), typeof(GradientView), propertyChanged: UpdateCanvas);

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

        static void OnGradientSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var gradientView = (GradientView)bindable;

            if (oldValue != null)
            {
                ((GradientElement)oldValue).Parent = null;
            }

            if (newValue != null)
            {
                ((GradientElement)newValue).Parent = gradientView;
            }

            gradientView.InvalidateSurface();
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
        }

        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            base.OnPaintSurface(e);

            var canvas = e.Surface.Canvas;
            canvas.Clear();

            if (GradientSource == null)
                return;

            if(Renderer == null)
                Renderer = new GradientRenderer(this);

            var context = Renderer.CreateContext(e);
            using (context.Paint)
            {
                foreach (var gradient in GradientSource.GetGradients())
                {
                    gradient.Measure(context.RenderRect.Width, context.RenderRect.Height);
                    Renderer.Render(context, gradient.Shader);
                }
            }
        }

        public void InvalidateCanvas()
        {
            InvalidateSurface();
        }
    }
}
