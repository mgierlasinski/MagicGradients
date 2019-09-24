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

        public static readonly BindableProperty GradientSourceProperty = BindableProperty.Create(
            nameof(GradientSource), typeof(ILinearGradientSource), typeof(LinearGradientView), propertyChanged:
            (bindable, oldValue, newValue) => (bindable as LinearGradientView)?.OnGradientSourceChanged());

        public ILinearGradientSource GradientSource
        {
            get => (ILinearGradientSource)GetValue(GradientSourceProperty);
            set => SetValue(GradientSourceProperty, value);
        }

        private void OnGradientSourceChanged()
        {
            InvalidateSurface();
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
                    var (startPoint, endPoint) = GetGradientPoints(info, gradient.Angle);

                    var orderedStops = gradient.Stops.OrderBy(x => x.Offset).ToArray();
                    var colors = orderedStops.Select(x => x.Color.ToSKColor()).ToArray();
                    var colorPos = orderedStops.Select(x => x.Offset).ToArray();

                    paint.Shader = SKShader.CreateLinearGradient(
                        startPoint,
                        endPoint,
                        colors,
                        colorPos,
                        SKShaderTileMode.Clamp);

                    canvas.DrawRect(info.Rect, paint);
                }
            }
        }

        private (SKPoint, SKPoint) GetGradientPoints(SKImageInfo info, double rotation)
        {
            var angle = rotation / 360.0;

            var a = info.Width * Math.Pow(Math.Sin(2 * Math.PI * ((angle + 0.75) / 2)), 2);
            var b = info.Height * Math.Pow(Math.Sin(2 * Math.PI * ((angle + 0.0) / 2)), 2);
            var c = info.Width * Math.Pow(Math.Sin(2 * Math.PI * ((angle + 0.25) / 2)), 2);
            var d = info.Height * Math.Pow(Math.Sin(2 * Math.PI * ((angle + 0.5) / 2)), 2);

            var start = new SKPoint(info.Width - (float)a, (float)b);
            var end = new SKPoint(info.Width - (float)c, (float)d);

            return (start, end);
        }
    }
}
