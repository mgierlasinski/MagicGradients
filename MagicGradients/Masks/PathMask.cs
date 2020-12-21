using MagicGradients.Renderers;
using SkiaSharp;
using Xamarin.Forms;

namespace MagicGradients.Masks
{
    public class PathMask : GradientMask
    {
        public static readonly BindableProperty DataProperty = BindableProperty.Create(nameof(Data),
            typeof(string), typeof(TextMask), 
            "M 0 -100 L 58.8 90.9, -95.1 -30.9, 95.1 -30.9, -58.8 80.9 Z");

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
            using (var path = SKPath.ParseSvgPathData(Data))
            {
                ClipPath(context, path);
            }
        }

        protected void ClipPath(RenderContext context, SKPath path)
        {
            using (new CanvasLock(context.Canvas))
            {
                path.GetTightBounds(out var bounds);

                context.Canvas.Translate((float)context.RenderRect.Width / 2, (float)context.RenderRect.Height / 2);

                if (Fill == PathFill.Center)
                    context.Canvas.Scale((float)context.RenderRect.Width / context.CanvasRect.Width, (float)context.RenderRect.Height / context.CanvasRect.Height);

                if (Fill == PathFill.Fill)
                    context.Canvas.Scale(context.RenderRect.Width / bounds.Width, context.RenderRect.Height / bounds.Height);

                context.Canvas.Translate(-bounds.MidX, -bounds.MidY);
                context.Canvas.ClipPath(path, ClipMode.ToSkOperation());
            }
        }

        public override string ToString() => "Path Mask";
    }
}
