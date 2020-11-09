using System;

namespace Playground.Features.CssPreviewer
{
    public class CssSnippet
    {
        public string Name { get; set; }
        public string Code => GetCode?.Invoke();
        public Func<string> GetCode { get; set; }
    }
}
