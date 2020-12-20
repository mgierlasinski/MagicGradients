using MagicGradients.Renderers;

namespace MagicGradients.Masks
{
    public interface IMask
    {
        void Clip(RenderContext context);
    }
}
