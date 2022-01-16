using System.Collections.Generic;

namespace MagicGradients.Masks
{
    public class GradientMask : IGradientMask
    {
        public ClipMode ClipMode { get; set; }
        public Stretch Stretch { get; set; }
        public bool IsActive { get; set; } = true;
    }

    public class RectangleMask : GradientMask, IRectangleMask
    {
        public Dimensions Size { get; set; }
        public Corners Corners { get; set; }
    }

    public class EllipseMask : RectangleMask, IEllipseMask
    {

    }

    public class PathMask : GradientMask, IPathMask
    {
        public string Data { get; set; }
    }

    public class TextMask : GradientMask, ITextMask
    {
        public string Text { get; set; }
        public string FontFamily { get; set; }
        public double FontSize { get; set; }
        public FontAttributes FontAttributes { get; set; }
        public TextAlignment HorizontalTextAlignment { get; set; }
        public TextAlignment VerticalTextAlignment { get; set; }
    }

    public class MaskCollection : GradientMask, IMaskCollection
    {
        public List<IGradientMask> Masks { get; set; }
        public IReadOnlyList<IGradientMask> GetMasks() => Masks;
    }
}
