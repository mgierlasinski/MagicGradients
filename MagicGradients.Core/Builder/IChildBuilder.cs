using System.Collections.Generic;

namespace MagicGradients.Builder
{
    public interface IChildBuilder
    {
        IGradientFactory Factory { get; set; }
        List<IGradientStop> Stops { get; }
        IGradient Construct();
    }
}
