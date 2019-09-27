using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Linq;
using Xamarin.Forms;

namespace MagicGradients
{
    public class LinearGradientView : SKCanvasView
    {
        static LinearGradientView()
        {
            StyleSheets.RegisterStyle("background", typeof(LinearGradientView), nameof(GradientSourceProperty));
        }

        public static readonly BindableProperty GradientSourceProperty = BindableProperty.Create(nameof(GradientSource), 
            typeof(ILinearGradientSource), typeof(LinearGradientView), propertyChanged: OnGradientSourceChanged);

        public ILinearGradientSource GradientSource
        {
            get => (ILinearGradientSource)GetValue(GradientSourceProperty);
            set => this.SetValue(GradientSourceProperty, value);
        }

        static void OnGradientSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var gradientView = (LinearGradientView)bindable;
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
                    var (startPoint, endPoint) = GetGradientPoints(info.Width, info.Height, gradient.Angle);

                    var orderedStops = gradient.Stops.OrderBy(x => x.Offset).ToArray();
                    var colors = orderedStops.Select(x => x.Color.ToSKColor()).ToArray();
                    var colorPos = orderedStops.Select(x => x.Offset).ToArray();
                    var tileMode = gradient.IsRepeating ? SKShaderTileMode.Repeat : SKShaderTileMode.Clamp;

                    paint.Shader = SKShader.CreateLinearGradient(
                        startPoint,
                        endPoint,
                        colors,
                        colorPos,
                        tileMode);

                    canvas.DrawRect(info.Rect, paint);
                }
            }
        }

        private (SKPoint, SKPoint) GetGradientPoints(int width, int height, double rotation)
        {
            var angle = rotation / 360.0;

            var a = width * Math.Pow(Math.Sin(2 * Math.PI * ((angle + 0.75) / 2)), 2);
            var b = height * Math.Pow(Math.Sin(2 * Math.PI * ((angle + 0.0) / 2)), 2);
            var c = width * Math.Pow(Math.Sin(2 * Math.PI * ((angle + 0.25) / 2)), 2);
            var d = height * Math.Pow(Math.Sin(2 * Math.PI * ((angle + 0.5) / 2)), 2);

            var start = new SKPoint(width - (float)a, (float)b);
            var end = new SKPoint(width - (float)c, (float)d);

            return (start, end);
        }
    }
}
