using MagicGradients.Builder;
using MagicGradients.Parser.TokenDefinitions;
using System.Collections.Generic;
using System.Linq;

namespace MagicGradients.Parser
{
    public class CssGradientParser
    {
        private readonly IGradientFactory _factory;
        private readonly ITokenDefinition[] _definitions;

        public CssGradientParser(IGradientFactory factory)
        {
            _factory = factory;
            _definitions = new ITokenDefinition[]
            {
                new LinearGradientDefinition(),
                new RadialGradientDefinition(), 
                new ColorHexDefinition(),
                new ColorChannelDefinition(),
                new ColorNameDefinition()
            };
        }
        
        private GradientBuilder ParseStylesheet(string css)
        {
            var builder = new GradientBuilder(_factory);

            if (string.IsNullOrWhiteSpace(css))
                return builder;

            var reader = new CssReader(css);

            while (reader.CanRead)
            {
                var token = reader.Read().Trim();

                var definition = _definitions.FirstOrDefault(x => x.IsMatch(token));
                definition?.Parse(reader, builder);

                reader.MoveNext();
            }

            return builder;
        }

        public IEnumerable<IGradient> Parse(string css)
        {
            var builder = ParseStylesheet(css);
            return builder.Build().Reverse();
        }

        public IEnumerable<TCast> ParseAs<TCast>(string css) where TCast : IGradient
        {
            var builder = ParseStylesheet(css);
            return builder.BuildAs<TCast>().Reverse();
        }
    }
}
