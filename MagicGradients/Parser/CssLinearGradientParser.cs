using System.Linq;

namespace MagicGradients.Parser
{
    public class CssLinearGradientParser
    {
        private readonly ITokenDefinition[] _definitions;
        private CssReader _reader;

        public CssLinearGradientParser()
        {
            _definitions = new ITokenDefinition[]
            {
                new LinearGradientDefinition(), 
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

            _reader = new CssReader(css);

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
