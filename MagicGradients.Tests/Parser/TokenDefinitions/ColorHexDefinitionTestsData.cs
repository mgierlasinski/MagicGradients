using System.Collections.Generic;
using Xamarin.Forms;

namespace MagicGradients.Tests.Parser.TokenDefinitions
{
    public class ColorHexDefinitionTestsData
    {
        public static IEnumerable<object[]> ColorParseData() => new List<object[]>
        {
            new object[] { "#ff0000", Color.FromHex("#ff0000"), -1 },
            new object[] { "#00ff00 30%", Color.FromHex("#00ff00"), 0.3f }
        };
    }
}
