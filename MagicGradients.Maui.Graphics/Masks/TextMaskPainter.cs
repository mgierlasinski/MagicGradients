using MagicGradients.Masks;
using MagicGradients.Maui.Graphics.Drawing;

namespace MagicGradients.Maui.Graphics.Masks
{
    public class TextMaskPainter : GradientMaskPainter, ITextMaskPainter<DrawContext>
    {
        public void Clip(TextMask mask, DrawContext context)
        {
            if (!mask.IsActive || string.IsNullOrEmpty(mask.Text))
                return;
            
            // TODO: apply clipping
        }
    }
}
