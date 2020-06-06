using Xunit;

namespace MagicGradients.Tests.Parser.TokenDefinitions
{
    public class RadialGradientDefinitionData : TheoryData<string, RadialGradient>
    {
        public RadialGradientDefinitionData()
        {
            Add("radial-gradient", new RadialGradient());
            Add(" radial-gradient ", new RadialGradient());
            Add("repeating-radial-gradient", new RadialGradient { IsRepeating = true });
            Add(" repeating-radial-gradient ", new RadialGradient { IsRepeating = true });
        }
    }
}
