using System.Collections.Generic;

namespace MagicGradients.Parser
{
    public class CssGradientParserSource : IGradientSource
    {
        private readonly CssGradientParser _parser = new();
        private List<IGradient> _gradients;

        public CssGradientParserSource()
        {
            _gradients = new List<IGradient>();
        }

        public CssGradientParserSource(string css)
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
