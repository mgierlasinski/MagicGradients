using System.Collections.Generic;
using Xamarin.Forms;

namespace MagicGradients.Masks
{
    public interface IGradientMask
    {
        ClipMode ClipMode { get; }
        Stretch Stretch { get; }
        bool IsActive { get; }
    }

    public interface IRectangleMask : IGradientMask
    {
        Dimensions Size { get; }
        Corners Corners { get; }
    }

    public interface IEllipseMask : IRectangleMask
    {
    }

    public interface IPathMask : IGradientMask
    {
        string Data { get; }
    }

    public interface ITextMask : IPathMask
    {
        string Text { get; }
        string FontFamily { get; }
        double FontSize { get; }
        FontAttributes FontAttributes { get; }
        TextAlignment HorizontalTextAlignment { get; }
        TextAlignment VerticalTextAlignment { get; }
    }

    public interface IMaskCollection
    {
        IReadOnlyList<IGradientMask> GetMasks();
    }
}
