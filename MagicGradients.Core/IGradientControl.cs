using MagicGradients.Masks;

namespace MagicGradients
{
    public interface IGradientControl
    {
        double Width { get; }
        IGradientSource GradientSource { get; set; }
        Dimensions GradientSize { get; set; }
        BackgroundRepeat GradientRepeat { get; set; }
        IGradientMask Mask { get; set; }
    }
}
