using MagicGradients.Masks;

namespace MagicGradients.Drawing
{
    public class MaskDrawable<TContext>
    {
        public IMaskPainter<EllipseMask, TContext> EllipsePainter { get; set; }
        public IMaskPainter<RectangleMask, TContext> RectanglePainter { get; set; }
        public IMaskPainter<TextMask, TContext> TextPainter { get; set; }
        public IMaskPainter<PathMask, TContext> PathPainter { get; set; }

        public MaskDrawable(
            IMaskPainter<EllipseMask, TContext> ellipsePainter,
            IMaskPainter<RectangleMask, TContext> rectanglePainter,
            IMaskPainter<TextMask, TContext> textPainter,
            IMaskPainter<PathMask, TContext> pathPainter)
        {
            EllipsePainter = ellipsePainter;
            RectanglePainter = rectanglePainter;
            TextPainter = textPainter;
            PathPainter = pathPainter;
        }

        public void Clip(GradientMask mask, TContext context)
        {
            switch (mask)
            {
                case EllipseMask ellipseMask:
                    EllipsePainter.Clip(ellipseMask, context);
                    break;
                case RectangleMask rectangleMask:
                    RectanglePainter.Clip(rectangleMask, context);
                    break;
                case TextMask textMask:
                    TextPainter.Clip(textMask, context);
                    break;
                case PathMask pathMask:
                    PathPainter.Clip(pathMask, context);
                    break;
                case MaskCollection maskCollection:
                    ClipCollection(maskCollection, context);
                    break;
            }
        }

        private void ClipCollection(MaskCollection maskCollection, TContext context)
        {
            foreach (var child in maskCollection.Masks)
            {
                Clip(child, context);
            }
        }
    }
}
