using System.Collections.Generic;
using System.Linq;

namespace MagicGradients.Parser
{
    public class CssGradientParserSource : IGradientSource
    {
        private readonly CssGradientParser _parser = new(GlobalSetup.Current.GradientFactory);
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
            _gradients = _parser.Parse(css).ToList();
        }

        public IReadOnlyList<IGradient> GetGradients()
        {
            return _gradients;
        }
    }
}
