using System.Collections.Generic;
using System.Drawing;

namespace Playground.Features.BattleTest.Services
{
    public interface IPickerColorsDataProvider
    {
        List<string> GetColorNames();
        Color GetColorByName(string colorName);
    }
}
