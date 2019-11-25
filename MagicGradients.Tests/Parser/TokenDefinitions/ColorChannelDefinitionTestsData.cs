using System.Collections.Generic;
using Xamarin.Forms;

namespace MagicGradients.Tests.Parser.TokenDefinitions
{
    public class ColorChannelDefinitionTestsData
    {
        public static IEnumerable<object[]> ColorParseData() => new List<object[]>
        {
            new object[] { "rgb(255, 0, 0) 25%", Color.FromRgb(255, 0, 0), 0.25f },
            new object[] { "rgba(255, 0, 0, 1)", Color.FromRgba(255, 0, 0, 255), -1 },
            new object[] { "hsl(180, 70%, 30%) 65%", Color.FromHsla(0.5, 0.7, 0.3), 0.65f },
            new object[] { "hsla(180, 70%, 30%, 0.5)", Color.FromHsla(0.5, 0.7, 0.3, 0.5), -1 }
        };
    }
}
