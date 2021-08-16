using MagicGradients.Masks;
using MagicGradients.Skia.Forms.Drawing;

namespace MagicGradients.Skia.Forms.Masks
{
    public class MaskDrawable
    {
        private readonly EllipseMaskPainter _ellipsePainter = new EllipseMaskPainter();
        private readonly RectangleMaskPainter _rectanglePainter = new RectangleMaskPainter();
        private readonly TextMaskPainter _textPainter = new TextMaskPainter();
        private readonly PathMaskPainter _pathPainter = new PathMaskPainter();

        public void Clip(GradientMask mask, DrawContext context)
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

        private void ClipCollection(MaskCollection maskCollection, DrawContext context)
        {
            foreach (var child in maskCollection.Masks)
            {
                Clip(child, context);
            }
        }
    }
}
