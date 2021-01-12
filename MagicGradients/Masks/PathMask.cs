using System;
using MagicGradients.Renderers;
using SkiaSharp;
using Xamarin.Forms;

namespace MagicGradients.Masks
{
    public class PathMask : GradientMask
    {
        public static readonly BindableProperty DataProperty = BindableProperty.Create(nameof(Data),
            typeof(string), typeof(TextMask));

        public static readonly BindableProperty FillProperty = BindableProperty.Create(nameof(Fill),
            typeof(PathFill), typeof(TextMask), PathFill.Center);

        public string Data
        {
            get => (string)GetValue(DataProperty);
            set => SetValue(DataProperty, value);
        }

        public PathFill Fill
        {
            get => (PathFill)GetValue(FillProperty);
            set => SetValue(FillProperty, value);
        }

        public override void Clip(RenderContext context)
        {
            if (!IsActive || string.IsNullOrEmpty(Data))
                return;

            using var path = SKPath.ParseSvgPathData(Data);
            ClipPath(context, path);
        }

        protected void ClipPath(RenderContext context, SKPath path)
        {
            using (new CanvasLock(context.Canvas))
            {
                path.GetTightBounds(out var bounds);

                context.Canvas.Translate((float)context.RenderRect.Width / 2, (float)context.RenderRect.Height / 2);

                if (Fill == PathFill.Center)
                {
                    context.Canvas.Scale((float)context.RenderRect.Width / context.CanvasRect.Width, (float)context.RenderRect.Height / context.CanvasRect.Height);
                }
                else
                {
                    var scaleX = context.RenderRect.Width / bounds.Width;
                    var scaleY = context.RenderRect.Height / bounds.Height;

                    if (Fill == PathFill.AspectFit)
                        context.Canvas.Scale(Math.Min(scaleX, scaleY));

                    if (Fill == PathFill.AspectFill)
                        context.Canvas.Scale(Math.Max(scaleX, scaleY));

                    if (Fill == PathFill.Fill)
                        context.Canvas.Scale(scaleX, scaleY);
                }

                context.Canvas.Translate(-bounds.MidX, -bounds.MidY);
                context.Canvas.ClipPath(path, ClipMode.ToSkOperation());
            }
        }

        public override string ToString() => "Path Mask";
    }
}
