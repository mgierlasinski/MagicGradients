using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Playground.Services
{
    public class PickerColorsDataProvider: IPickerColorsDataProvider
    {
        private Dictionary<string, Color> _namesToColors { get; } = new Dictionary<string, Color>
        {
            {"White", Color.White},
            {"Black", Color.Black}
        };

        public Color GetColorByName(string colorName) => _namesToColors[colorName];

        public List<string> GetColorNames() => _namesToColors.Keys.ToList();
    }
}
