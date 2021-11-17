using Xunit;

namespace MagicGradients.Tests.Parser.TokenDefinitions
{
    public class LinearGradientDefinitionData : TheoryData<string, LinearGradient>
    {
        public LinearGradientDefinitionData()
        {
            Add("linear-gradient", new LinearGradient());
            Add(" linear-gradient ", new LinearGradient());
            Add("repeating-linear-gradient", new LinearGradient { IsRepeating = true });
            Add(" repeating-linear-gradient ", new LinearGradient { IsRepeating = true });
            Add("linear-gradient(245deg,", new LinearGradient { Angle = 65 });
            Add("linear-gradient(to right", new LinearGradient { Angle = 270 });
            Add("repeating-linear-gradient(245deg,", new LinearGradient { Angle = 65, IsRepeating = true });
            Add("repeating-linear-gradient(to right", new LinearGradient { Angle = 270, IsRepeating = true });
        }
    }
}