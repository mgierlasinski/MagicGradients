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

    public class GenericGradientSource : IGradientSource
    {
        private readonly IReadOnlyList<IGradient> _gradients;

        public GenericGradientSource(IReadOnlyList<IGradient> gradients)
        {
            _gradients = gradients;
        }
        
        public IReadOnlyList<IGradient> GetGradients()
        {
            return _gradients;
        }
    }
}
