using MagicGradients.Renderers;
using SkiaSharp;

namespace MagicGradients.Masks
{
    public class EllipseMask : GradientElement, IMask
    {
        public void Clip(RenderContext context)
        {
            var rect = new SKRoundRect(context.RenderRect, 
                (float)context.RenderRect.Width / 2, 
                (float)context.RenderRect.Height / 2);

            context.Canvas.ClipRoundRect(rect, antialias: true);
        }

        public override string ToString() => "Ellipse Mask";
    }
}
