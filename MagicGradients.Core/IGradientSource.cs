using MagicGradients.Converters;
using System.Collections.Generic;
using System.ComponentModel;

namespace MagicGradients
{
    [TypeConverter(typeof(CssGradientSourceTypeConverter))]
    public interface IGradientSource
    {
        IReadOnlyList<IGradient> GetGradients();
    }
}
