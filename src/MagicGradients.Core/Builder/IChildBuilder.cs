using System.Collections.Generic;

namespace MagicGradients.Builder
{
    public interface IChildBuilder
    {
        IGradientFactory Factory { get; set; }
        List<IGradientStop> Stops { get; }
        void AddConstructed(List<IGradient> gradients);
    }
}
