using MagicGradients.Parser;
using System.Collections.Generic;

namespace MagicGradients.Builder
{
    public class CssGradientBuilder : StopsBuilder<CssGradientBuilder>, IChildBuilder
    {
        protected override CssGradientBuilder Instance => this;

        internal string StyleSheet { get; }

        public CssGradientBuilder(string styleSheet)
        {
            StyleSheet = styleSheet;
        }

        public void AddConstructed(List<IGradient> gradients)
        {
            gradients.AddRange(new CssGradientParser().Parse(StyleSheet));
        }
    }
}
