using MagicGradients.Drawing;

namespace MagicGradients.Masks
{
    public class TextMaskPainter : IMaskPainter<ITextMask, DrawContext>
    {
        public void Clip(ITextMask mask, DrawContext context)
        {
            if (!mask.IsActive || string.IsNullOrEmpty(mask.Text))
                return;
            
            // TODO: apply clipping
        }
    }
}
