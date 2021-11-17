using MagicGradients.Renderers;
using SkiaSharp;

namespace MagicGradients.Masks
{
    public class EllipseMask : RectangleMask
    {
        public override void Clip(RenderContext context)
        {
            if(!IsActive)
                return;

            var ellipse = GetEllipse(context);
            ClipRoundRect(context, ellipse);
        }

        private SKRoundRect GetEllipse(RenderContext context)
        {
            var width = (int)Size.Width.GetDrawPixels(context.CanvasRect.Width, context.PixelScaling);
            var height = (int)Size.Height.GetDrawPixels(context.CanvasRect.Height, context.PixelScaling);

            var bounds = new SKRectI(0, 0, width, height);
            return new SKRoundRect(bounds, (float)width / 2, (float)height / 2);
        }
    }
}
