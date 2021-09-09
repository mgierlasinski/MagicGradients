using MagicGradients.Graphics.Drawing;
using MagicGradients.Masks;

namespace MagicGradients.Graphics.Masks
{
    public class PathMaskPainter : GradientMaskPainter, IMaskPainter<PathMask, DrawContext>
    {
        public void Clip(PathMask mask, DrawContext context)
        {
            if (!mask.IsActive || string.IsNullOrEmpty(mask.Data))
                return;

            // TODO: apply clipping
        }
    }
}
