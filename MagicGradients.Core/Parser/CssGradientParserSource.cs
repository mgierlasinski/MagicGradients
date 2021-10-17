using System.Collections.Generic;
using System.Linq;

namespace MagicGradients.Parser
{
    public class CssGradientParserSource : IGradientSource
    {
        private readonly List<IGradient> _gradients;
        private readonly CssGradientParser _parser = new CssGradientParser(GlobalSetup.Current.GradientFactory);

        public CssGradientParserSource(string css)
        {
            _gradients = _parser.Parse(css).ToList();
        }

        public IReadOnlyList<IGradient> GetGradients()
        {
            return _gradients;
        }
    }
}
