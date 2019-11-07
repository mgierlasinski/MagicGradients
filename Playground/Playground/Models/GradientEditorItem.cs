using MagicGradients;

namespace Playground.Models
{
    public class GradientEditorItem
    {
        public string Type { get; set; }

        public IGradientSource StopsSource { get; set; }
    }
}
