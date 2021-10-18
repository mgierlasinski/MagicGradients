using MagicGradients.Parser;
using System;
using System.Linq;

namespace MagicGradients.Builder
{
    public class CssGradientBuilder : StopsBuilder<CssGradientBuilder>, IChildBuilder
    {
        protected override CssGradientBuilder Instance => this;

        internal string StyleSheet { get; set; }

        public CssGradientBuilder(string styleSheet)
        {
            StyleSheet = styleSheet;
        }

        public IGradient Construct()
        {
            var parsed = new CssGradientParser(Factory).Parse(StyleSheet).ToArray();
            if (parsed.Length != 1)
            {
                throw new InvalidOperationException("StyleSheet must contain single gradient function.");
            }
            return parsed.First();
        }
    }
}
