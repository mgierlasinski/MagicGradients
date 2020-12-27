using MagicGradients.Renderers;
using SkiaSharp;

namespace MagicGradients.Masks
{
    public class EllipseMask : GradientMask
    {
        public override void Clip(RenderContext context)
        {
            if(!IsActive)
                return;

            var rect = new SKRoundRect(context.RenderRect, 
                (float)context.RenderRect.Width / 2, 
                (float)context.RenderRect.Height / 2);

            context.Canvas.ClipRoundRect(rect, ClipMode.ToSkOperation(), true);
        }

        public override string ToString() => "Ellipse Mask";
    }
}
