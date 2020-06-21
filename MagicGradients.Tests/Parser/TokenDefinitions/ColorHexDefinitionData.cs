using Xamarin.Forms;
using Xunit;

namespace MagicGradients.Tests.Parser.TokenDefinitions
{
    public class ColorHexDefinitionData : TheoryData<string, Color, double>
    {
        public ColorHexDefinitionData()
        {
            Add("#ff0000", Color.FromHex("#ff0000"), -1);
            Add("#00ff00 30%", Color.FromHex("#00ff00"), 0.3d);
        }
    }
}
