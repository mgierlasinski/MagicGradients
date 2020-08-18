using MagicGradients.Renderers;
using SkiaSharp;
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
        }

        public static readonly BindableProperty GradientSourceProperty = BindableProperty.Create(nameof(GradientSource), 
            typeof(IGradientSource), typeof(GradientView), propertyChanged: OnGradientSourceChanged);

        public static readonly BindableProperty GradientSizeProperty = BindableProperty.Create(nameof(GradientSize),
            typeof(Dimensions), typeof(GradientView), propertyChanged: OnGradientSizeChanged);

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

        static void OnGradientSizeChanged(BindableObject bindable, object oldValue, object newValue)
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

            using (var paint = new SKPaint())
            {
                var context = new RenderContext(canvas, paint, e.Info, GradientSize);

                foreach (var gradient in GradientSource.GetGradients())
                {
                    gradient.Measure(e.Info.Width, e.Info.Height);
                    gradient.Render(context);
                }
            }
        }

        public void InvalidateCanvas()
        {
            InvalidateSurface();
        }
    }
}
