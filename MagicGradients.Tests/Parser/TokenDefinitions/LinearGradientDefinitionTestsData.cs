using System.Collections.Generic;

namespace MagicGradients.Tests.Parser.TokenDefinitions
{
    public static class LinearGradientDefinitionTestsData
    {
        public static IEnumerable<object[]> GradientParseData() => new List<object[]>
        {
            new object[] {"linear-gradient", new LinearGradient()},
            new object[] {" linear-gradient ", new LinearGradient()},
            new object[] {"repeating-linear-gradient", new LinearGradient {IsRepeating = true}},
            new object[] {" repeating-linear-gradient ", new LinearGradient {IsRepeating = true}},
            new object[] {"linear-gradient(245deg,", new LinearGradient {Angle = 65}},
            new object[] {"linear-gradient(to right", new LinearGradient {Angle = 270}},
            new object[] {"repeating-linear-gradient(245deg,", new LinearGradient {Angle = 65, IsRepeating = true}},
            new object[] {"repeating-linear-gradient(to right", new LinearGradient {Angle = 270, IsRepeating = true}},
        };
    }
}