using Xamarin.Forms;
using Xunit;

namespace MagicGradients.Tests.Parser.TokenDefinitions
{
    public class ColorNameDefinitionData : TheoryData<string, Color, double>
    {
        public ColorNameDefinitionData()
        {
            Add("red", Color.Red, -1);
            Add("Color.blue", Color.Blue, -1);
            Add("orange 60%", Color.Orange, 0.6d);
        }
    }
}
