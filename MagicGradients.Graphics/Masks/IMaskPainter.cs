namespace MagicGradients.Graphics.Masks
{
    public interface IMaskPainter<TMask, TContext>
    {
        void Clip(TMask mask, TContext context);
    }
}
