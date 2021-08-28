using MagicGradients.Masks;

namespace MagicGradients.Maui.Graphics.Masks
{
    public class MaskDrawable<TContext>
    {
        private readonly IEllipseMaskPainter<TContext> _ellipsePainter;
        private readonly IRectangleMaskPainter<TContext> _rectanglePainter;
        private readonly ITextMaskPainter<TContext> _textPainter;
        private readonly IPathMaskPainter<TContext> _pathPainter;

        public MaskDrawable(
            IEllipseMaskPainter<TContext> ellipsePainter,
            IRectangleMaskPainter<TContext> rectanglePainter,
            ITextMaskPainter<TContext> textPainter,
            IPathMaskPainter<TContext> pathPainter)
        {
            _ellipsePainter = ellipsePainter;
            _rectanglePainter = rectanglePainter;
            _textPainter = textPainter;
            _pathPainter = pathPainter;
        }

        public void Clip(GradientMask mask, TContext context)
        {
            switch (mask)
            {
                case EllipseMask ellipseMask:
                    _ellipsePainter.Clip(ellipseMask, context);
                    break;
                case RectangleMask rectangleMask:
                    _rectanglePainter.Clip(rectangleMask, context);
                    break;
                case TextMask textMask:
                    _textPainter.Clip(textMask, context);
                    break;
                case PathMask pathMask:
                    _pathPainter.Clip(pathMask, context);
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
