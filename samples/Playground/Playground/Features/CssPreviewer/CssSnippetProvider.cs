using Playground.Extensions;

namespace Playground.Features.CssPreviewer
{
    public interface ICssSnippetProvider
    {
        CssSnippet[] GetCssSnippets();
    }

    public class CssSnippetProvider : ICssSnippetProvider
    {
        public CssSnippet[] GetCssSnippets() => new[]
        {
            new CssSnippet
            {
                Name = "Linear (colors only)",
                GetCode = () => string.Format("linear-gradient({0}, {1}, {2})",
                    ColorUtils.GetRandom().ToHex(),
                    ColorUtils.GetRandom().ToHex(),
                    ColorUtils.GetRandom().ToHex())
            },
            new CssSnippet
            {
                Name = "Linear (with angle)",
                GetCode = () => string.Format("linear-gradient(45deg, {0} 0%, {1} 50%, {2} 100%)",
                    ColorUtils.GetRandom().ToHex(),
                    ColorUtils.GetRandom().ToHex(),
                    ColorUtils.GetRandom().ToHex())
            },
            new CssSnippet
            {
                Name = "Radial (colors only)",
                GetCode = () => string.Format("radial-gradient({0}, {1}, {2})",
                    ColorUtils.GetRandom().ToHex(),
                    ColorUtils.GetRandom().ToHex(),
                    ColorUtils.GetRandom().ToHex())
            },
            new CssSnippet
            {
                Name = "Radial (with shape)",
                GetCode = () => string.Format("radial-gradient(circle, {0} 0%, {1} 50%, {2} 100%)",
                    ColorUtils.GetRandom().ToHex(),
                    ColorUtils.GetRandom().ToHex(),
                    ColorUtils.GetRandom().ToHex())
            }
        };
    }
}
