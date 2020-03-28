using MagicGradients;
using System.Drawing;

namespace Playground.Models
{
    public class BattleItem
    {
        public string Text { get; set; }
        public IGradientSource GradientSource { get; set; }
        public Color TextColor { get; set; }
    }
}
