using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace MagicGradients
{
    public class GradientView : SKCanvasView
    {
        static GradientView()
        {
            StyleSheets.RegisterStyle("background", typeof(GradientView), nameof(GradientSourceProperty));
        }

        public static readonly BindableProperty GradientSourceProperty = BindableProperty.Create(nameof(GradientSource), 
            typeof(IGradientSource), typeof(GradientView), propertyChanged: OnGradientSourceChanged);

        public IGradientSource GradientSource
        {
            get => (IGradientSource)GetValue(GradientSourceProperty);
            set => this.SetValue(GradientSourceProperty, value);
        }

        static void OnGradientSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var gradientView = (GradientView)bindable;
            gradientView.InvalidateSurface();
        }

        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            base.OnPaintSurface(e);

            var info = e.Info;
            var surface = e.Surface;
            var canvas = surface.Canvas;

            canvas.Clear();

            if (GradientSource == null)
                return;

            using (var paint = new SKPaint())
            {
                foreach (var gradient in GradientSource.GetGradients())
                {
                    paint.Shader = gradient.CreateShader(paint, info);
                    canvas.DrawRect(info.Rect, paint);
                }
            }
        }
    }
}
