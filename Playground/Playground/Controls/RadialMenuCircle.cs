using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;

namespace Playground.Controls
{
    public class RadialMenuCircle : SKCanvasView
    {
        public RadialMenu RadialMenu => Parent as RadialMenu;

        private readonly List<SKPath> _touchPaths = new List<SKPath>();

        protected override void OnTouch(SKTouchEventArgs e)
        {
            RadialMenu.SelectedIndex = -1;

            for (var i = 0; i < _touchPaths.Count; i++)
            {
                if(_touchPaths[i].Contains(e.Location.X, e.Location.Y))
                {
                    RadialMenu.SelectedIndex = i;
                    InvalidateSurface();
                    return;
                }
            }
        }

        protected override void OnPaintSurface(SKPaintSurfaceEventArgs args)
        {
            if (RadialMenu?.Items == null)
                return;

            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();
            _touchPaths.Clear();

            SKPoint center = new SKPoint(info.Width / 2, info.Height / 2);
            float explodeOffset = 30;
            float radius = Math.Min(info.Width / 2, info.Height / 2) - 2 * explodeOffset;
            SKRect rect = new SKRect(center.X - radius, center.Y - radius,
                                     center.X + radius, center.Y + radius);
            
            float startAngle = 0;
            float sweepAngle = 360f / RadialMenu.Items.Count;

            using (SKPaint fillPaint = CreateFillPaint())
            using (SKPaint outlinePaint = CreateOutlinePaint())
            {
                foreach (var item in RadialMenu.Items)
                {
                    var path = new SKPath();
                    path.MoveTo(center);
                    path.ArcTo(rect, startAngle, sweepAngle, false);
                    path.Close();

                    fillPaint.Color = SKColor.Parse(item);
                    
                    // Calculate "explode" transform
                    float angle = startAngle + 0.5f * sweepAngle;
                    float x = explodeOffset * (float)Math.Cos(Math.PI * angle / 180);
                    float y = explodeOffset * (float)Math.Sin(Math.PI * angle / 180);

                    canvas.Save();

                    if (RadialMenu.SelectedIndex == _touchPaths.Count)
                        canvas.Translate(x, y);

                    canvas.DrawPath(path, fillPaint);
                    canvas.DrawPath(path, outlinePaint);
                    canvas.Restore();

                    startAngle += sweepAngle;

                    _touchPaths.Add(path);
                }

                //if(RadialMenu.SelectedIndex > -1)
                //{
                //    outlinePaint.Color = SKColors.Yellow;
                //    canvas.DrawPath(_touchPaths[RadialMenu.SelectedIndex], outlinePaint);
                //}
            }

            DrawInnerCircle(canvas, ref center);
        }

        private SKPaint CreateFillPaint()
        {
            var fillPaint = new SKPaint();
            fillPaint.Style = SKPaintStyle.Fill;
            fillPaint.IsAntialias = true;

            return fillPaint;
        }

        private SKPaint CreateOutlinePaint()
        {
            var outlinePaint = new SKPaint();
            outlinePaint.Style = SKPaintStyle.Stroke;
            outlinePaint.StrokeWidth = 4;
            outlinePaint.Color = SKColors.White;
            outlinePaint.IsAntialias = true;

            return outlinePaint;
        }

        private void DrawInnerCircle(SKCanvas canvas, ref SKPoint center)
        {
            using (SKPaint fillPaint = CreateFillPaint())
            using (SKPaint outlinePaint = CreateOutlinePaint())
            {
                fillPaint.BlendMode = SKBlendMode.Clear;
                outlinePaint.BlendMode = SKBlendMode.SrcATop;

                canvas.DrawCircle(center.X, center.Y, 100, fillPaint);
                canvas.DrawCircle(center.X, center.Y, 102, outlinePaint);
            }
        }
    }
}
