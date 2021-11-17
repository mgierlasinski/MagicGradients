using MagicGradients.Renderers;
using SkiaSharp;
using Xamarin.Forms;

namespace MagicGradients.Masks
{
    public class PathMask : GradientMask
    {
        public static readonly BindableProperty DataProperty = BindableProperty.Create(nameof(Data),
            typeof(string), typeof(PathMask));

        public string Data
        {
            get => (string)GetValue(DataProperty);
            set => SetValue(DataProperty, value);
        }

        public override void Clip(RenderContext context)
        {
            if (!IsActive || string.IsNullOrEmpty(Data))
                return;

            using var path = SKPath.ParseSvgPathData(Data);
            ClipPath(context, path);
        }

        protected internal void ClipPath(RenderContext context, SKPath path)
        {
            path.GetTightBounds(out var bounds);

            using (new CanvasLock(context.Canvas))
            {
                LayoutBounds(context, bounds, true);
                context.Canvas.ClipPath(path, ClipMode.ToSkOperation());
            }
        }
    }
}
