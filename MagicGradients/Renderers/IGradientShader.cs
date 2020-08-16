using SkiaSharp;

namespace MagicGradients.Renderers
{
    public interface IGradientShader
    {
        SKShader Create(RenderContext context);
    }
}
