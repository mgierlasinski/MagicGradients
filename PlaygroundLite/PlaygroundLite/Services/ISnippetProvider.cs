using PlaygroundLite.Models;

namespace PlaygroundLite.Services
{
    public interface ISnippetProvider
    {
        CssSnippet[] GetCssSnippets();
    }
}
