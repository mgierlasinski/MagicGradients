using System.Collections.Generic;
using Xamarin.Forms;

namespace MagicGradients.Tests.Parser.TokenDefinitions
{
    public class ColorNameDefinitionTestsData
    {
        public static IEnumerable<object[]> ColorParseData() => new List<object[]>
        {
            new object[] { "red", Color.Red, 0 },
            new object[] { "Color.blue", Color.Blue, 0 },
            new object[] { "orange 60%", Color.Orange, 0.6f }
        };
    }
}
