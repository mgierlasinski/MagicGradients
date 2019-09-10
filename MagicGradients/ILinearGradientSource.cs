using System.Collections.Generic;

namespace MagicGradients
{
    public interface ILinearGradientSource
    {
        IEnumerable<LinearGradient> GetGradients();
    }
}
