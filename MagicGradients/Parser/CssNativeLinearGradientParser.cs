using System.Linq;
using MagicGradients.Parser.Readers;
using MagicGradients.Parser.TokenDefinitions;

namespace MagicGradients.Parser
{
    public class CssNativeLinearGradientParser
    {
        private readonly ITokenDefinition[] _definitions;
        private CssNativeReader _reader;

        public CssNativeLinearGradientParser()
        {
            _definitions = new ITokenDefinition[]
            {
                new LinearNativeGradientDefinition(),
                new ColorRgbDefinition(),
                new ColorHslDefinition()
            };
        }

        public LinearGradient[] ParseCss(string css)
        {
            var builder = new LinearGradientBuilder();

            if (string.IsNullOrWhiteSpace(css))
            {
                return builder.Build();
            }

            _reader = new CssNativeReader(css);

            while (_reader.CanRead)
            {
                var token = _reader.Read();

                var definition = _definitions.FirstOrDefault(x => x.IsMatch(token));
                definition?.Parse(_reader, builder);

                _reader.MoveNext();
            }

            return builder.Build().Reverse().ToArray();
        }
    }
}
