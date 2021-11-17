using MagicGradients.Masks;

namespace MagicGradients.Drawing
{
    public interface IMaskPainter<TMask, TContext>
    {
        void Clip(TMask mask, TContext context);
    }

    public class MaskDrawable<TContext>
    {
        public IMaskPainter<IEllipseMask, TContext> EllipsePainter { get; set; }
        public IMaskPainter<IRectangleMask, TContext> RectanglePainter { get; set; }
        public IMaskPainter<ITextMask, TContext> TextPainter { get; set; }
        public IMaskPainter<IPathMask, TContext> PathPainter { get; set; }

        public MaskDrawable(
            IMaskPainter<IEllipseMask, TContext> ellipsePainter,
            IMaskPainter<IRectangleMask, TContext> rectanglePainter,
            IMaskPainter<ITextMask, TContext> textPainter,
            IMaskPainter<IPathMask, TContext> pathPainter)
        {
            EllipsePainter = ellipsePainter;
            RectanglePainter = rectanglePainter;
            TextPainter = textPainter;
            PathPainter = pathPainter;
        }

        public void Clip(IGradientMask mask, TContext context)
        {
            switch (mask)
            {
                case IEllipseMask ellipseMask:
                    EllipsePainter.Clip(ellipseMask, context);
                    break;
                case IRectangleMask rectangleMask:
                    RectanglePainter.Clip(rectangleMask, context);
                    break;
                case ITextMask textMask:
                    TextPainter.Clip(textMask, context);
                    break;
                case IPathMask pathMask:
                    PathPainter.Clip(pathMask, context);
                    break;
                case IMaskCollection maskCollection:
                    ClipCollection(maskCollection, context);
                    break;
            }
        }

        private void ClipCollection(IMaskCollection maskCollection, TContext context)
        {
            foreach (var child in maskCollection.GetMasks())
            {
                Clip(child, context);
            }
        }
    }
}
