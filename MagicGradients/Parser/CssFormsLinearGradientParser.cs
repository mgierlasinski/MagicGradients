using System.Linq;
using MagicGradients.Parser.Readers;
using MagicGradients.Parser.TokenDefinitions;

namespace MagicGradients.Parser
{
    public class CssFormsLinearGradientParser
    {
        private readonly LinearFormsGradientDefinition _formsGradient;

        public CssFormsLinearGradientParser()
        {
            _formsGradient = new LinearFormsGradientDefinition();
        }

        public LinearGradient[] ParseCss(string css)
        {
            var builder = new LinearGradientBuilder();

            if (string.IsNullOrWhiteSpace(css))
                return builder.Build();

            var reader = new CssFormsReader(css);

            while (reader.CanRead)
            {
                _formsGradient.Parse(reader.Read(), builder);
                reader.MoveNext();
            }

            return builder.Build().Reverse().ToArray();
        }
    }
}
