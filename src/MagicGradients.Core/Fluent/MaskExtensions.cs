using MagicGradients.Masks;
using System.Collections.Generic;

namespace MagicGradients.Fluent
{
    public static class MaskExtensions
    {
        public static IGradientControl Mask(this IGradientControl control, IGradientMask mask)
        {
            control.Mask = mask;
            return control;
        }

        public static IGradientControl Mask(this IGradientControl control, params IGradientMask[] masks)
        {
            control.Mask = new MaskCollection { Masks = new List<IGradientMask>(masks) };
            return control;
        }
    }
}
