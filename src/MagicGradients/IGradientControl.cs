using MagicGradients.Masks;

namespace MagicGradients
{
    public interface IGradientControl
    {
        IGradientSource GradientSource { get; set; }
        Dimensions GradientSize { get; set; }
        BackgroundRepeat GradientRepeat { get; set; }
        GradientMask Mask { get; set; }
    }
}
