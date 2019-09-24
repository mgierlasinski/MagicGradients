using MagicGradients.Parser.TokenDefinitions;
using System.Linq;

namespace MagicGradients.Parser
{
    public class CssLinearGradientParser
    {
        private readonly ITokenDefinition[] _definitions;

        public CssLinearGradientParser()
        {
            _definitions = new ITokenDefinition[]
            {
                new LinearGradientDefinition(),
                new ColorDefinition()
            };
        }

        public LinearGradient[] ParseCss(string css)
        {
            var builder = new LinearGradientBuilder();

            if (string.IsNullOrWhiteSpace(css))
            {
                return builder.Build();
            }

            var reader = new CssReader(css);

            while (reader.CanRead)
            {
                var token = reader.Read().Trim();

                var definition = _definitions.FirstOrDefault(x => x.IsMatch(token));
                definition?.Parse(reader, builder);

                reader.MoveNext();
            }

            return builder.Build().Reverse().ToArray();
        }
    }
}
