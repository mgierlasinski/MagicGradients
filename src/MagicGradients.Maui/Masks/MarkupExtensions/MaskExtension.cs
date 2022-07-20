using MagicGradients.Masks;
using Stretch = MagicGradients.Masks.Stretch;

namespace MagicGradients.Forms.Masks
{
    public class MaskExtension
    {
        public ClipMode ClipMode { get; set; }
        public Stretch Stretch { get; set; }

        protected void FillValues(GradientMask mask)
        {
            mask.ClipMode = ClipMode;
            mask.Stretch = Stretch;
        }
    }
}
