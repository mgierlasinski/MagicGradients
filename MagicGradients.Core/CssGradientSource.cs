using MagicGradients.Parser;
using System.Collections.Generic;

namespace MagicGradients
{
    public class CssGradientSource : IGradientSource
    {
        private readonly CssGradientParser _parser = new();
        private List<IGradient> _gradients;

        public CssGradientSource()
        {
            _gradients = new List<IGradient>();
        }

        public CssGradientSource(string css)
        {
            Parse(css);
        }

        public void Parse(string css)
        {
            _gradients = _parser.Parse(css);
        }

        public IReadOnlyList<IGradient> GetGradients()
        {
            return _gradients;
        }
    }
}
