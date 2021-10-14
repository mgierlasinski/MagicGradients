using MagicGradients.Drawing;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Graphics.Skia;
using SkiaSharp;
using System;

namespace MagicGradients.Forms.Skia.Drawing
{
    public class SkiaCanvasEx : SkiaCanvas
    {
        private SKShader _shader;

        public void ClipPath(SKPath path, SKClipOperation operation)
        {
            path.Transform(SKMatrix.CreateScale(CurrentState.ScaleX, CurrentState.ScaleY));
            Canvas.ClipPath(path, operation);
        }

        public override void SetFillPaint(Paint paint, RectangleF rectangle)
        {
            if (_shader != null)
            {
                CurrentState.SetFillPaintShader(null);
                _shader.Dispose();
                _shader = null;
            }

            if (paint is LinearGradientPaintEx linearGradientPaint)
            {
                DrawLinearGradient(linearGradientPaint, rectangle);
            }
            else if (paint is RadialGradientPaintEx radialGradientPaint)
            {
                DrawRadialGradient(radialGradientPaint, rectangle);
            }
            else
            {
                base.SetFillPaint(paint, rectangle);
            }
        }
        
        private void DrawLinearGradient(LinearGradientPaintEx paint, RectangleF rectangle)
        {
            var colors = new SKColor[paint.GradientStops.Length];
            var stops = new float[colors.Length];
            var vStops = paint.GetSortedStops();

            for (var i = 0; i < vStops.Length; i++)
            {
                colors[i] = vStops[i].Color.ToColor(CurrentState.Alpha);
                stops[i] = vStops[i].Offset;
            }

            try
            {
                CurrentState.FillColor = Colors.White;
                _shader = SKShader.CreateLinearGradient(
                    GetPoint(paint.StartPoint, rectangle),
                    GetPoint(paint.EndPoint, rectangle),
                    colors,
                    stops,
                    paint.IsRepeating ? SKShaderTileMode.Repeat : SKShaderTileMode.Clamp);
                CurrentState.SetFillPaintShader(_shader);
            }
            catch (Exception exc)
            {
                Logger.Debug(exc);
                FillColor = paint.BlendStartAndEndColors();
            }
        }

        private void DrawRadialGradient(RadialGradientPaintEx paint, RectangleF rectangle)
        {
            var colors = new SKColor[paint.GradientStops.Length];
            var stops = new float[colors.Length];
            var vStops = paint.GetSortedStops();

            for (var i = 0; i < vStops.Length; i++)
            {
                colors[i] = vStops[i].Color.ToColor(CurrentState.Alpha);
                stops[i] = vStops[i].Offset;
            }

            var center = GetPoint(paint.Center, rectangle);
            var radiusX = (float)(paint.Size.Width * rectangle.Width);
            var radiusY = (float)(paint.Size.Height * rectangle.Height);

            try
            {
                CurrentState.FillColor = Colors.White;
                _shader = SKShader.CreateRadialGradient(
                    center,
                    Math.Min(radiusX, radiusY),
                    colors,
                    stops,
                    paint.IsRepeating ? SKShaderTileMode.Repeat : SKShaderTileMode.Clamp,
                    GetScaleMatrix(center, radiusX, radiusY));
                CurrentState.SetFillPaintShader(_shader);
            }
            catch (Exception exc)
            {
                Logger.Debug(exc);
                FillColor = paint.BlendStartAndEndColors();
            }
        }

        private SKPoint GetPoint(Point point, RectangleF rectangle)
        {
            return new SKPoint(
                (float)(point.X * rectangle.Width) + rectangle.X,
                (float)(point.Y * rectangle.Height) + rectangle.Y);
        }

        private SKMatrix GetScaleMatrix(SKPoint center, float radiusX, float radiusY)
        {
            if (radiusX > radiusY)
                return SKMatrix.CreateScale(radiusX / radiusY, 1f, center.X, center.Y);

            if (radiusY > radiusX)
                return SKMatrix.CreateScale(1f, radiusY / radiusX, center.X, center.Y);

            return SKMatrix.CreateIdentity();
        }

        //protected override void NativeScale(float xFactor, float yFactor)
        //{
        //    CurrentState.SetScale(Math.Abs(xFactor), Math.Abs(yFactor));
        //    Canvas.Scale(Math.Abs(xFactor), Math.Abs(yFactor));
        //}

        //protected override void NativeTranslate(float tx, float ty)
        //{
        //    Canvas.Translate(tx, ty);
        //}

        //protected override void NativeConcatenateTransform(AffineTransform transform)
        //{
        //    var values = new float[9];
        //    Canvas.TotalMatrix.GetValues(values);

        //    var matrix = new SKMatrix { Values = values };
        //    var newMatrix = matrix.PostConcat(transform.AsMatrix());

        //    Canvas.SetMatrix(newMatrix);
        //}
    }
}