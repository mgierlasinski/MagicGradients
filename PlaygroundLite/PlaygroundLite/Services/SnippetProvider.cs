using PlaygroundLite.Models;

namespace PlaygroundLite.Services
{
    public class SnippetProvider : ISnippetProvider
    {
        public CssSnippet[] GetCssSnippets() => new[]
        {
            new CssSnippet
            {
                Name = "Linear gradient",
                Code = string.Format("linear-gradient(90deg, {0} 0%, {1} 50%, {2} 100%)",
                    ColorUtils.GetRandom().ToHex(),
                    ColorUtils.GetRandom().ToHex(),
                    ColorUtils.GetRandom().ToHex())
            },
            new CssSnippet
            {
                Name = "Radial gradient",
                Code = string.Format("radial-gradient(circle, {0} 0%, {1} 50%, {2} 100%)",
                    ColorUtils.GetRandom().ToHex(),
                    ColorUtils.GetRandom().ToHex(),
                    ColorUtils.GetRandom().ToHex())
            }
        };
    }
}
