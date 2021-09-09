using MagicGradients.Graphics.Drawing;
using MagicGradients.Masks;

namespace MagicGradients.Graphics.Masks
{
    public class TextMaskPainter : GradientMaskPainter, IMaskPainter<TextMask, DrawContext>
    {
        public void Clip(TextMask mask, DrawContext context)
        {
            if (!mask.IsActive || string.IsNullOrEmpty(mask.Text))
                return;
            
            // TODO: apply clipping
        }
    }
}
