using System.Collections.Generic;
using System.Drawing;

namespace Playground.Services
{
    public interface IPickerColorsDataProvider
    {
        List<string> GetColorNames();
        Color GetColorByName(string colorName);
    }
}
