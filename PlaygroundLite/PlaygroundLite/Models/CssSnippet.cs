using System;

namespace PlaygroundLite.Models
{
    public class CssSnippet
    {
        public string Name { get; set; }
        public string Code => GetCode?.Invoke();
        public Func<string> GetCode { get; set; }
    }
}
