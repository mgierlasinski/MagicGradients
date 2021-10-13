using MagicGradients.Drawing;

namespace MagicGradients.Masks
{
    public class TextMaskPainter : MaskPainter, IMaskPainter<TextMask, DrawContext>
    {
        public void Clip(TextMask mask, DrawContext context)
        {
            if (!mask.IsActive || string.IsNullOrEmpty(mask.Text))
                return;
            
            // TODO: apply clipping
        }
    }
}
