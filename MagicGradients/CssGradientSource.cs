using MagicGradients.Parser;
using System.Collections.Generic;

namespace MagicGradients
{
    public class CssGradientSource : ILinearGradientSource
    {
        public string Stylesheet { get; set; }

        public IEnumerable<LinearGradient> GetGradients()
        {
            return new CssLinearGradientParser().ParseCss(Stylesheet);
        }
    }
}
