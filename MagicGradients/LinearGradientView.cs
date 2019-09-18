using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.StyleSheets;

namespace MagicGradients
{
    public class LinearGradientView : SKCanvasView
    {

        static LinearGradientView()
        {
            var stylePropertyInfo = typeof(Xamarin.Forms.Internals.Registrar).GetProperty("StyleProperties",
                BindingFlags.Static | BindingFlags.NonPublic);
            if (stylePropertyInfo == null)
                return;

            var styleProperties = stylePropertyInfo.GetValue(null);

            var styleAttributeType = typeof(StyleSheet).Assembly.GetType("Xamarin.Forms.StyleSheets.StylePropertyAttribute");
            var styleAttributeInstance = Activator.CreateInstance(styleAttributeType, "gradient",
                typeof(LinearGradientView), nameof(GradientSourceProperty));

            var dictionaryAdd = styleProperties.GetType().GetMethod("Add");
            if (dictionaryAdd == null)
                return;

            var styleListType = typeof(List<>).MakeGenericType(styleAttributeType);
            var styleList = (IList)Activator.CreateInstance(styleListType);

            styleList.Add(styleAttributeInstance);
            dictionaryAdd.Invoke(styleProperties, new object[] { "gradient", styleList });
        }


        public static readonly BindableProperty GradientSourceProperty = BindableProperty.Create(
            nameof(GradientSource), typeof(CssFormsGradientSource), typeof(LinearGradientView));

        public CssFormsGradientSource GradientSource
        {
            get => (CssFormsGradientSource)GetValue(GradientSourceProperty);
            set => SetValue(GradientSourceProperty, value);
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

        private (SKPoint, SKPoint) GetGradientPoints(SKImageInfo info, int rotation)
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
