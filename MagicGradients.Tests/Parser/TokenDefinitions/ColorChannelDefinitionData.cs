using Xamarin.Forms;
using Xunit;

namespace MagicGradients.Tests.Parser.TokenDefinitions
{
    public class ColorChannelDefinitionData : TheoryData<string, Color, double>
    {
        public ColorChannelDefinitionData()
        {
            Add("rgb(255, 0, 0) 25%", Color.FromRgb(255, 0, 0), 0.25d);
            Add("rgba(255, 0, 0, 1)", Color.FromRgba(255, 0, 0, 255), -1);
            Add("hsl(180, 70%, 30%) 65%", Color.FromHsla(0.5, 0.7, 0.3), 0.65d);
            Add("hsla(180, 70%, 30%, 0.5)", Color.FromHsla(0.5, 0.7, 0.3, 0.5), -1);
        }
    }
}
