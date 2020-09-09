using SkiaSharp;

namespace MagicGradients.Renderers
{
    public interface IGradientShader
    {
        SKShader Create(RenderContext context);
        double CalculateRenderOffset(double offset, int width, int height);
    }
}
